using Autofac;
using Mickey.EntityFramework.Repositories;

namespace Mickey.EntityFramework.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterTypesForAutoRepository<TDbContext>(this ContainerBuilder builder)
            where TDbContext : IDbContext
        {
            builder.RegisterType<TDbContext>().AsSelf().AsImplementedInterfaces().InstancePerRequest();
            AutoRepositoryRegistrar.RegisterForDbContext(typeof(TDbContext), builder);
        }
    }
}
