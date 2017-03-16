using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQAuthenticatedContext : BaseContext
    {
        public QQAuthenticatedContext(IOwinContext context, QQUser user, QQAccessToken accessToken)
            : base(context)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (accessToken == null)
                throw new ArgumentNullException("accessToken");

            User = user;
            AccessToken = accessToken;
        }

        public QQUser User { get; private set; }

        public QQAccessToken AccessToken { get; private set; }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }

        public virtual void SetIdentity(string authenticationType, AuthenticationProperties properties)
        {
            Identity = new ClaimsIdentity(
                    authenticationType,
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, User.Id, QQConstants.XmlSchemaString, authenticationType));
            Identity.AddClaim(new Claim(ClaimTypes.AuthenticationInstant, JsonConvert.SerializeObject(User), QQConstants.XmlSchemaString, authenticationType));
            Identity.AddClaim(new Claim(QQConstants.AccessTokenClaimType, JsonConvert.SerializeObject(AccessToken), QQConstants.XmlSchemaString, authenticationType));
            Properties = properties;
        }
    }
}
