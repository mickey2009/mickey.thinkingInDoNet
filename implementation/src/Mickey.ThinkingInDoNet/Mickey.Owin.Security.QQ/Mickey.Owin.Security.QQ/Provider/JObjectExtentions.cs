using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    internal static class JObjectExtentions
    {
        public static string TryGetStringValue(this JObject obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            JToken value;
            return obj.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }

        public static int TryGetIntValue(this JObject obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            JToken value;
            int result;
            if (obj.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out value) && int.TryParse(value.ToString(), out result))
                return result;
            return 0;
        }
    }
}
