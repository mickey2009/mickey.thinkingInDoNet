using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQAccessToken
    {
        public QQAccessToken()
        {
            Provider = QQConstants.DefaultAuthenticationType;
        }

        public string Provider { get; private set; }

        public string AccessToken { get; set; }

        public string Expiration { get; set; }

        public string RefreshToken { get; set; }

        public string OpenId { get; set; }
    }
}
