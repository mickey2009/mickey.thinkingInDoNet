using Microsoft.Framework.Configuration;

namespace Mickey.Core.Infrastructure.Configuration
{
    public static class AppSettingsExtentions
    {
        public static IConfigurationBuilder AddAppSettings(this IConfigurationBuilder configuration)
        {
            configuration.Add(new AppSettingConfigurationProvider());
            return configuration;
        }
    }
}
