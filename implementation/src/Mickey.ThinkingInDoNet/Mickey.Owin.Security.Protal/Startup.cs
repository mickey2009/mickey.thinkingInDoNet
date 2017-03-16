using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mickey.Owin.Security.Protal.Startup))]
namespace Mickey.Owin.Security.Protal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
