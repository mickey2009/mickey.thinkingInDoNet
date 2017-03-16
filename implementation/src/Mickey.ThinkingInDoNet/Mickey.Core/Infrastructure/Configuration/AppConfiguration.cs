using Mickey.Core.ComponentModel;
using Microsoft.Framework.Configuration;
using System;

namespace Mickey.Core.Infrastructure.Configuration
{
    public class AppConfiguration
    {
        static IConfiguration _Current;

        /// <summary>
        /// 获取当前配置。
        /// </summary>
        public static IConfiguration Current
        {
            get
            {
                if (_Current == null)
                    throw new InvalidOperationException("必须先调用Set方法赋值");

                return _Current;
            }
        }

        public static void Set(IConfiguration configuration)
        {
            Requires.NotNull(configuration, nameof(configuration));
            _Current = configuration;
        }
    }
}
