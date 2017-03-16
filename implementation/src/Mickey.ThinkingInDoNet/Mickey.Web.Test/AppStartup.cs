using Autofac;
using Mickey.EntityFramework.Extensions;
using Mickey.Web.Extensions;
using Mickey.Web.Startup;
using Mickey.Web.Test.Core.Managers;
using Mickey.Web.Test.Core.Services;
using Mickey.Web.Test.Core.Stores;
using Owin;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mickey.Web.Test
{
    public class AppStartup : StartupBase
    {
        public override void OnInitialize(StartupContext context)
        {
            Database.SetInitializer(new AppDbContextInitializer());
        }

        public override void OnPreStart(StartupContext context, ContainerBuilder builder)
        {
            builder.RegisterType<AppSession>().As<IAppSession>().InstancePerRequest();
            builder.RegisterType<UserStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<SignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterTypesForMvc(Assembly.GetExecutingAssembly());
            builder.RegisterTypesForUnitOfWork();
            builder.RegisterTypesForAutoRepository<AppDbContext>();
            builder.RegisterTypesForApplicationService(typeof(UserManager).Assembly);
        }

        public override void OnPostStart(StartupContext context, IAppBuilder app, IContainer container)
        {
            ConfigureMvc(app);
        }

        public override void OnShutdown(StartupContext context)
        {
        }

        static void ConfigureMvc(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapMvcAttributeRoutes();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}