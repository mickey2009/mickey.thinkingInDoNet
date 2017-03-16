using Autofac;
using Autofac.Integration.Mvc;
using Mickey.Core.Infrastructure.Configuration;
using Mickey.Core.Infrastructure.DependencyInjection;
using Mickey.Web.DependencyInjection;
using Microsoft.Framework.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Mvc;

namespace Mickey.Web.Startup
{
    public abstract class StartupBase : IStartup
    {
        public void Initialize(StartupContext context)
        {
            OnInitialize(context);
        }

        public abstract void OnInitialize(StartupContext context);

        public void PreStart(StartupContext context, ContainerBuilder builder)
        {
            builder.RegisterType<OwinContextLifetimeScopeProvider>().As<IIocContainerProvider>().InstancePerRequest();
            OnPreStart(context, builder);
        }

        public abstract void OnPreStart(StartupContext context, ContainerBuilder builder);

        public void PostStart(StartupContext context, IAppBuilder app, IContainer container)
        {
            OnPostStart(context, app, container);
            app.UseAutofacMiddleware(container);
            UseCookieAuthentication(app, context.Configuration);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        public abstract void OnPostStart(StartupContext context, IAppBuilder app, IContainer container);

        public void Shutdown(StartupContext context)
        {
            OnShutdown(context);
        }

        public abstract void OnShutdown(StartupContext context);

        static void UseCookieAuthentication(IAppBuilder app, IConfiguration config)
        {
            var options = new CookieAuthenticationOptions
            {
                AuthenticationType = Constants.ApplicationCookie,
                CookieName = config.GetRequiredItem(Constants.ConfigKeys.AuthCookieName),
                CookiePath = "/",
                LoginPath = new PathString(config.Get<string>(Constants.ConfigKeys.AuthLoginUrl, "/Login")),
                LogoutPath = new PathString(config.Get<string>(Constants.ConfigKeys.AuthLoginUrl, "/Logout")),
                ExpireTimeSpan = TimeSpan.FromDays(config.Get<int>(Constants.ConfigKeys.AuthLoginCookieExpiresDay, 7))
            };
            var cookieDomain = config.Get<string>(Constants.ConfigKeys.AuthCookieDomain);
            if (!string.IsNullOrWhiteSpace(cookieDomain))
            {
                options.CookieDomain = cookieDomain;
            }
            app.UseCookieAuthentication(options);
        }
    }
}
