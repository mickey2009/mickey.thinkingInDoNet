using Microsoft.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Mickey.Core.Infrastructure.Configuration
{
    public class AppSettingConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Load(System.Configuration.ConfigurationManager.AppSettings);
        }

        void Load(NameValueCollection appSettings)
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var key in appSettings.AllKeys)
            {
                data.Add(key, appSettings[key]);
            }

            Data = data;
        }
    }
}
