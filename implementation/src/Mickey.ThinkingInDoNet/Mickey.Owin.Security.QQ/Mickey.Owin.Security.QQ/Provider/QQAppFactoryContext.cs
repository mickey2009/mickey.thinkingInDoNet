using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQAppFactoryContext : BaseContext<QQAuthenticationOptions>
    {
        public QQAppFactoryContext(IOwinContext context, QQAuthenticationOptions options,
            AuthenticationProperties properties)
            : base(context, options)
        {
            Properties = properties;
        }

        public AuthenticationProperties Properties { get; private set; }
    }
}
