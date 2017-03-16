using Mickey.Owin.Security.QQ.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ
{
    public class QQAuthenticationOptions : AuthenticationOptions
    {
        public QQAuthenticationOptions()
            : base(QQConstants.DefaultAuthenticationType)
        {
            Caption = QQConstants.DefaultAuthenticationType;
            CallbackPath = new PathString("/signin-QQ");
            AuthenticationMode = AuthenticationMode.Passive;
            Scope = "get_user_info";
            BackchannelTimeout = TimeSpan.FromSeconds(60);
        }

        public string AppId { get; set; }

        public string AppKey { get; set; }

        public TimeSpan BackchannelTimeout { get; set; }

        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        public PathString CallbackPath { get; set; }

        public string SignInAsAuthenticationType { get; set; }

        public IQQAuthenticationProvider Provider { get; set; }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        public string Scope { get; private set; }

        public IStateStore StateStore { get; set; }

        public Func<QQAppFactoryContext, QQConnectApp> AppFactory { get; set; }
    }
}
