using Mickey.Core.Common;
using Mickey.Core.ComponentModel;
using Microsoft.Framework.Configuration;
using System.Collections.Generic;

namespace Mickey.Core.Infrastructure.Configuration
{
    public static class ConfigurationExtentions
    {
        public static string Get(this IConfiguration configuration, string key, string defaultValue = null)
        {
            Requires.NotNull(configuration, nameof(configuration));
            Requires.NotNull(key, nameof(key));

            string value = configuration[key];
            return value ?? defaultValue;
        }

        public static T Get<T>(this IConfiguration configuration, string key, T defaultValue = default(T))
        {
            Requires.NotNull(configuration, nameof(configuration));
            Requires.NotNull(key, nameof(key));

            string value = configuration[key];
            return value == null ? defaultValue : value.As<T>();
        }

        public static string GetRequiredItem(this IConfiguration configuration, string key)
        {
            Requires.NotNull(configuration, nameof(configuration));
            Requires.NotNull(key, nameof(key));

            string value = configuration[key];
            if (value == null)
                throw new KeyNotFoundException("无法找到指定的配置项: " + key);

            return value;
        }

        public static T GetRequiredItem<T>(this IConfiguration configuration, string key)
        {
            Requires.NotNull(configuration, nameof(configuration));
            Requires.NotNull(key, nameof(key));

            string value = configuration[key];
            if (value == null)
                throw new KeyNotFoundException("无法找到指定的配置项: " + key);

            return value.As<T>();
        }

        public static void Set(this IConfiguration configuration, string key, string value)
        {
            Requires.NotNull(configuration, nameof(configuration));
            configuration[key] = value;
        }
    }
}
