using Mickey.Owin.Security.QQ.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Helpers;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ
{
    public class QQAuthenticationHandler : AuthenticationHandler<QQAuthenticationOptions>
    {
        static readonly string _UserInfoApiEndpointTemplate = "https://graph.qq.com/user/get_user_info?access_token={0}&openid={1}&oauth_consumer_key={2}";
        static readonly string _OpenIdEndpointTemplate = "https://graph.qq.com/oauth2.0/me?access_token={0}";
        static readonly string _TokenEndpointTemplate = "https://graph.qq.com/oauth2.0/token?grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}";
        static readonly string _AuthorizeEndpointTemplate = "https://graph.qq.com/oauth2.0/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}";
        HttpClient m_HttpClient;
        ILogger m_Logger;

        public QQAuthenticationHandler(HttpClient client, ILogger logger)
        {
            m_HttpClient = client;
            m_Logger = logger;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            AuthenticationProperties properties = null;

            try
            {
                string code = null;
                string state = null;

                IReadableStringCollection query = Request.Query;

                IList<string> values = query.GetValues("state");
                if (values != null && values.Count == 1)
                {
                    state = await Options.StateStore.RetrieveAsync(values[0]) as string;
                }

                properties = Options.StateDataFormat.Unprotect(state);
                if (properties == null)
                    return null;

                // OAuth2 10.12 CSRF
                if (!ValidateCorrelationId(properties, m_Logger))
                {
                    return new AuthenticationTicket(null, properties);
                }

                values = query.GetValues("code");
                if (values != null && values.Count == 1)
                {
                    code = values[0];
                }
                if (code == null)
                {
                    m_Logger.WriteVerbose("用户拒绝授权。");
                    // Null if the remote server returns an error.
                    return new AuthenticationTicket(null, properties);
                }

                string requestPrefix = string.Concat(Request.Scheme, "://", Request.Host);
                string redirectUri = string.Concat(requestPrefix, Request.PathBase, Options.CallbackPath);

                var app = Options.AppFactory.Invoke(new QQAppFactoryContext(Context, Options, properties));
                var tokenResult = await GetAccessToken(app, code, redirectUri);
                if (tokenResult.Code != 0)
                {
                    m_Logger.WriteWarning("获取AccessToken时发生错误: ErrorCode[{0}], ErrorMessage[{1}]",
                        tokenResult.Code.ToString(), tokenResult.Message);
                    return new AuthenticationTicket(null, properties);
                }

                var accessToken = tokenResult.Data;
                var openIdResult = await GetOpenId(app, accessToken);
                if (openIdResult.Code != 0)
                {
                    m_Logger.WriteWarning("获取OpenId时发生错误: Response[0]", openIdResult.Data);
                    return new AuthenticationTicket(null, properties);
                }

                var userResult = await GetQQUser(app, accessToken);
                if (userResult.Code != 0)
                {
                    m_Logger.WriteWarning("获取UserInfo时发生错误: ErrorCode[{0}], ErrorMessage[{1}]",
                        userResult.Code.ToString(), userResult.Message);
                    return new AuthenticationTicket(null, properties);
                }
                var context = new QQAuthenticatedContext(Context, userResult.Data, accessToken);
                context.SetIdentity(Options.AuthenticationType, properties);
                await Options.Provider.Authenticated(context);
                return new AuthenticationTicket(context.Identity, context.Properties);
            }
            catch (Exception ex)
            {
                m_Logger.WriteError("Authentication failed", ex);
                return new AuthenticationTicket(null, properties);
            }
        }

        async Task<InvokedResult<QQUser>> GetQQUser(QQConnectApp app, QQAccessToken accessToken)
        {
            string userInfoAddress = string.Format(_UserInfoApiEndpointTemplate, Uri.EscapeDataString(accessToken.AccessToken), Uri.EscapeDataString(accessToken.OpenId), app.Id);
            HttpResponseMessage graphResponse = await m_HttpClient.GetAsync(userInfoAddress, Request.CallCancelled);
            graphResponse.EnsureSuccessStatusCode();
            var text = await graphResponse.Content.ReadAsStringAsync();
            //TODO: 增加扩展点。
            var userObject = JObject.Parse(text);
            QQUser user = new QQUser(userObject);
            user.Id = accessToken.OpenId;
            return new InvokedResult<QQUser>
            {
                Code = userObject.TryGetIntValue("ret"),
                Message = userObject.TryGetStringValue("msg"),
                Data = user
            };
        }

        async Task<InvokedResult<string>> GetOpenId(QQConnectApp app, QQAccessToken accessToken)
        {
            string openIdEndpoint = string.Format(_OpenIdEndpointTemplate, Uri.EscapeDataString(accessToken.AccessToken));
            HttpResponseMessage openIdResponse = await m_HttpClient.GetAsync(openIdEndpoint, Request.CallCancelled);
            openIdResponse.EnsureSuccessStatusCode();
            var text = await openIdResponse.Content.ReadAsStringAsync();
            var re = "\"openid\"\\s*:\\s*\"(?<openid>[^\"]+)\"";
            var match = Regex.Match(text, re);
            if (match.Success)
            {
                accessToken.OpenId = match.Groups["openid"].Value;
                return new InvokedResult<string> { Code = 0, Data = accessToken.OpenId };
            }
            return new InvokedResult<string> { Code = 1, Data = text, Message = "无法解析OpenId" };
        }

        async Task<InvokedResult<QQAccessToken>> GetAccessToken(QQConnectApp app, string code, string redirectUri)
        {
            string tokenEndpoint = string.Format(_TokenEndpointTemplate,
                    Uri.EscapeDataString(code), Uri.EscapeDataString(redirectUri),
                    Uri.EscapeDataString(app.Id), Uri.EscapeDataString(app.Key));
            HttpResponseMessage tokenResponse = await m_HttpClient.GetAsync(tokenEndpoint, Request.CallCancelled);
            tokenResponse.EnsureSuccessStatusCode();
            string text = await tokenResponse.Content.ReadAsStringAsync();
            var token = ParseJObject(text);
            var result = new InvokedResult<QQAccessToken>();
            result.Code = token.TryGetIntValue("code");
            result.Message = token.TryGetStringValue("msg");
            result.Data = new QQAccessToken
            {
                AccessToken = token.TryGetStringValue("access_token"),
                Expiration = DateTime.Now.AddSeconds(token.TryGetIntValue("expires_in")).ToString("yyyy-MM-dd HH:mm:ss"),
                RefreshToken = token.TryGetStringValue("refresh_token")
            };
            return result;
        }

        static JObject ParseJObject(string text)
        {
            var result = new JObject();
            if (string.IsNullOrWhiteSpace(text))
                return result;

            var form = WebHelpers.ParseForm(text);
            foreach (var item in form)
            {
                var value = item.Value == null || item.Value.Length == 0 ? string.Empty : item.Value[0].ToString();
                result.Add(new JProperty(item.Key, value));
            }
            return result;
        }

        protected override async Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
                return;

            AuthenticationResponseChallenge challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
            if (challenge != null)
            {
                string baseUri = string.Concat(Request.Scheme, Uri.SchemeDelimiter, Request.Host, Request.PathBase);
                string currentUri = string.Concat(baseUri, Request.Path, Request.QueryString);
                string redirectUri = baseUri + Options.CallbackPath;

                AuthenticationProperties properties = challenge.Properties;
                if (string.IsNullOrEmpty(properties.RedirectUri))
                {
                    properties.RedirectUri = currentUri;
                }

                // OAuth2 10.12 CSRF
                GenerateCorrelationId(properties);
                var state = Options.StateDataFormat.Protect(properties);
                //Protect生成的state可能会因为参数过长被拒绝 —— 微信有这种情况。
                state = await Options.StateStore.StoreAsync(state);
                var app = Options.AppFactory.Invoke(new QQAppFactoryContext(Context, Options, properties));
                var redirectingContext = new QQRedirectingContext(Context, Options,
                    properties, redirectUri);
                await Options.Provider.Redirecting(redirectingContext);
                string authorizationEndpoint = string.Format(_AuthorizeEndpointTemplate,
                        Uri.EscapeDataString(app.Id),
                        Uri.EscapeDataString(redirectingContext.RedirectUri),
                        Uri.EscapeDataString(Options.Scope),
                        Uri.EscapeDataString(state));

                var redirectContext = new QQApplyRedirectContext(
                    Context, Options,
                    properties, authorizationEndpoint);
                Options.Provider.ApplyRedirect(redirectContext);
            }
            return;
        }

        public override async Task<bool> InvokeAsync()
        {
            return await InvokeReplyPathAsync();
        }

        public virtual Task InvokeAuthenticatedFailedReplyAsync(IOwinContext context)
        {
            context.Response.Write("身份验证失败，无法处理。");
            return Task.FromResult<object>(null);
        }

        async Task<bool> InvokeReplyPathAsync()
        {
            if (Options.CallbackPath.HasValue && Options.CallbackPath == Request.Path)
            {
                AuthenticationTicket ticket = await AuthenticateAsync();
                if (ticket == null)
                {
                    m_Logger.WriteWarning("身份验证失败，AuthenticationTicket为空。");
                    await InvokeAuthenticatedFailedReplyAsync(Context);
                    return true;
                }

                var context = new QQReturnEndpointContext(Context, ticket);
                context.SignInAsAuthenticationType = Options.SignInAsAuthenticationType;
                context.RedirectUri = ticket.Properties.RedirectUri;

                await Options.Provider.ReturnEndpoint(context);

                if (context.SignInAsAuthenticationType != null &&
                    context.Identity != null)
                {
                    ClaimsIdentity grantIdentity = context.Identity;
                    if (!string.Equals(grantIdentity.AuthenticationType, context.SignInAsAuthenticationType, StringComparison.Ordinal))
                    {
                        grantIdentity = new ClaimsIdentity(grantIdentity.Claims, context.SignInAsAuthenticationType, grantIdentity.NameClaimType, grantIdentity.RoleClaimType);
                    }
                    Context.Authentication.SignIn(context.Properties, grantIdentity);
                }

                if (!context.IsRequestCompleted && context.RedirectUri != null)
                {
                    string redirectUri = context.RedirectUri;
                    if (context.Identity == null)
                    {
                        // add a redirect hint that sign-in failed in some way
                        redirectUri = WebUtilities.AddQueryString(redirectUri, "error", "access_denied");
                    }
                    Response.Redirect(redirectUri);
                    context.RequestCompleted();
                }

                return context.IsRequestCompleted;
            }
            return false;
        }
    }
}
