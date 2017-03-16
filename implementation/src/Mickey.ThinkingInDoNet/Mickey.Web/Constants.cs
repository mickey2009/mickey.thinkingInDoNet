namespace Mickey.Web
{
    public static class Constants
    {
        public static class ConfigKeys
        {
            public static readonly string AppName = "host.AppName";
            public static readonly string AuthUseCookieAuthentication = "Auth:UseCookieAuthentication";
            public static readonly string AuthCookieDomain = "Auth:CookieDomain";
            public static readonly string AuthCookieName = "Auth:CookieName";
            public static readonly string AuthLoginUrl = "Auth:LoginUrl";
            public static readonly string AuthLogoutUrl = "Auth:LogoutUrl";
            public static readonly string AuthLoginCookieExpiresDay = "Auth:LoginCookieExpiresDay";
            public static readonly string L10nEnabled = "L10n:Enabled";

            public static readonly string DefaultControllerName = "Mvc:DefaultController";
            public static readonly string DefaultActionName = "Mvc:DefaultAction";

            public static readonly string DefaultErrorMessage = "UI:DefaultErrorMessage";
            public static readonly string DefaultErrorView = "UI:DefaultErrorView";
            public static readonly string EnableDbContextLog = "DbContext:EnableLog";
        }

        internal static readonly string IServiceProviderKey = "Mickey.Web:IServiceProvider";

        internal static readonly string ApplicationCookie = "ApplicationCookie";
    }
}
