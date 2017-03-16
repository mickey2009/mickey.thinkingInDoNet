using Mickey.Owin.Security.QQ.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ
{
    public class QQAuthenticationMiddleware : AuthenticationMiddleware<QQAuthenticationOptions>
    {
        readonly ILogger m_Logger;
        readonly HttpClient m_HttpClient;

        public QQAuthenticationMiddleware(OwinMiddleware next,
            IAppBuilder app,
            QQAuthenticationOptions options)
            : base(next, options)
        {
            if (options == null)
                throw new ArgumentNullException("options");
            if (options.AppFactory == null && (string.IsNullOrWhiteSpace(options.AppId) || string.IsNullOrWhiteSpace(options.AppKey)))
                throw new ArgumentException("无法找到应用信息");

            m_Logger = app.CreateLogger<QQAuthenticationMiddleware>();

            if (Options.Provider == null)
            {
                Options.Provider = new QQAuthenticationProvider();
            }
            if (Options.StateDataFormat == null)
            {
                IDataProtector dataProtector = app.CreateDataProtector(
                    typeof(QQAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");
                Options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }
            if (String.IsNullOrEmpty(Options.SignInAsAuthenticationType))
            {
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            }
            if (Options.StateStore == null)
            {
                Options.StateStore = new InUrlStateStore();
            }
            if (Options.AppFactory == null)
            {
                Options.AppFactory = c => new QQConnectApp { Id = options.AppId, Key = options.AppKey };
            }

            m_HttpClient = new HttpClient();
            m_HttpClient.Timeout = Options.BackchannelTimeout;
            m_HttpClient.MaxResponseContentBufferSize = 1024 * 1024 * 1; // 1 MB
        }

        protected override AuthenticationHandler<QQAuthenticationOptions> CreateHandler()
        {
            return new QQAuthenticationHandler(m_HttpClient, m_Logger);
        }
    }
}
