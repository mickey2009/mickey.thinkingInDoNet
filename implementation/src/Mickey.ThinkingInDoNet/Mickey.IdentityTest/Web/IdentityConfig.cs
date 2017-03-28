using Mickey.IdentityTest.Core.Infrastructure;
using Mickey.IdentityTest.Core.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mickey.IdentityTest.Web
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new IdentityDbContextInitializer());

            app.CreatePerOwinContext(() => new IdentityDbContext());
            app.CreatePerOwinContext<UserManager>(UserManager.Create);
            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}