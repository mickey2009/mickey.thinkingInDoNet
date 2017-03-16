using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mickey.OAuth2.MvcAuth.Startup))]
namespace Mickey.OAuth2.MvcAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
