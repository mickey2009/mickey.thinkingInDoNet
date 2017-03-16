using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQUser
    {
        public string Provider { get; private set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string PortraitUrl { get; set; }

        public Dictionary<string, string> Properties { get; private set; }

        [JsonIgnore]
        public JObject Origin { get; set; }

        public QQUser() { }

        public QQUser(JObject user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            Properties = new Dictionary<string, string>();
            Name = user.TryGetStringValue("nickname");
            PortraitUrl = user.TryGetStringValue("figureurl_qq_2");
            Properties.Add("Gender", user.TryGetStringValue("gender"));
            Properties.Add("BirthYear", user.TryGetStringValue("year"));
            Properties.Add("City", user.TryGetStringValue("city"));
            Properties.Add("Province", user.TryGetStringValue("province"));
            Origin = user;
            Provider = QQConstants.DefaultAuthenticationType;
        }
    }
}
