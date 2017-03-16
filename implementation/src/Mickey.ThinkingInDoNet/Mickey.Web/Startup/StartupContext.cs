using Mickey.Core.ComponentModel;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Logging;
using System;

namespace Mickey.Web.Startup
{
    public class StartupContext
    {
        public ILogger Logger { get; internal set; }

        public IConfiguration Configuration { get; private set; }

        public void SetConfiguration(IConfiguration config)
        {
            Requires.NotNull(config, nameof(config));
            if (Configuration != null)
                throw new InvalidOperationException("不能重复设置Configuration");

            Configuration = config;
        }
    }
}
