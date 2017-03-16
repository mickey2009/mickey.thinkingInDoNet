using Mickey.Core.Infrastructure.Configuration;
using System.Diagnostics;
using System.Web;

namespace Mickey.Web
{
    public static class WebEnvironment
    {
        static readonly bool _InDevelopMode;

        static WebEnvironment()
        {
            _InDevelopMode = AppConfiguration.Current.Get<bool>("DevelopMode", false);
        }

        public static bool IsDebuging
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled)
                    return true;

                return Debugger.IsAttached;
            }
        }

        public static bool InDevelopMode
        {
            get
            {
                return _InDevelopMode;
            }
        }
    }
}
