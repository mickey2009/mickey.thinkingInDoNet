using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Mvc;
using Mickey.Core.Application;
using Mickey.Core.ComponentModel;
using Mickey.Core.Domain.Uow;
using Mickey.EntityFramework.Uow;
using Mickey.Web.Mvc;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Logging;
using System.Reflection;

namespace Mickey.Web.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterTypesForMvc(this ContainerBuilder builder, Assembly assembly)
        {
            Requires.NotNull(assembly, nameof(assembly));
            builder.RegisterControllers(assembly).OnActivated(e =>
            {
                var controller = e.Instance as BaseController;
                controller.Configuration = e.Context.Resolve<IConfiguration>();
                controller.Logger = e.Context.Resolve<ILogger>();
                controller.ErrorResultFactory = e.Context.Resolve<IErrorResultFactory>();
            });
            builder.RegisterModelBinders(assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            builder.RegisterType<DefaultErrorResultFactory>().As<IErrorResultFactory>();
        }

        public static void RegisterTypesForUnitOfWork(this ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>().InstancePerRequest();
            builder.RegisterType<CallContextCurrentUnitOfWorkProvider>().As<ICurrentUnitOfWorkProvider>();
            builder.RegisterType<UnitOfWorkDefaultOptions>().As<IUnitOfWorkDefaultOptions>();
            builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.Register(c => new UnitOfWorkInterceptor(c.Resolve<IUnitOfWorkManager>()));
        }


        public static void RegisterTypesForApplicationService(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => typeof(IApplicationService).IsAssignableFrom(t))
                   .EnableClassInterceptors()
                   .InterceptedBy(typeof(UnitOfWorkInterceptor));
        }
    }
}
