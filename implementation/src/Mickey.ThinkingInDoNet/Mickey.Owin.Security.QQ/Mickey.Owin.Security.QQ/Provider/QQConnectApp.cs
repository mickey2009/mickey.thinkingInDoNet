using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ
{
    public class QQConnectApp
    {
        public QQConnectApp()
        {
            Properties = new Dictionary<string, object>();
        }

        public QQConnectApp(IDictionary<string, object> properties)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            Properties = properties;
        }

        public string Id { get; set; }

        public string Key { get; set; }

        public IDictionary<string, object> Properties { get; private set; }
    }
}
