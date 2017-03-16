using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQApplyRedirectContext : BaseContext<QQAuthenticationOptions>
    {
        public QQApplyRedirectContext(IOwinContext context, QQAuthenticationOptions options,
            AuthenticationProperties properties, string redirectUri)
            : base(context, options)
        {
            RedirectUri = redirectUri;
            Properties = properties;
        }

        public string RedirectUri { get; private set; }

        public AuthenticationProperties Properties { get; private set; }
    }
}
