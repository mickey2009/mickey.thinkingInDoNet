using Autofac;
using Mickey.Core.Domain.Entities;
using System;

namespace Mickey.EntityFramework.Repositories
{
    public class AutoRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, ContainerBuilder builder)
        {
            var autoRepositoryAttr = AutoRepositoryTypes.Default;

            foreach (var entityTypeInfo in DbContextHelper.GetEntityTypeInfos(dbContextType))
            {
                var primaryKeyType = EntityHelper.GetPrimaryKeyType(entityTypeInfo.EntityType);

                if (primaryKeyType == typeof(string))
                {
                    var genericRepositoryType = autoRepositoryAttr.RepositoryInterface.MakeGenericType(entityTypeInfo.EntityType);

                    var implType = autoRepositoryAttr.RepositoryImplementation.GetGenericArguments().Length == 1
                           ? autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityTypeInfo.EntityType)
                           : autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityTypeInfo.DeclaringType, entityTypeInfo.EntityType);

                    builder.RegisterType(implType).As(genericRepositoryType);
                }

                var genericRepositoryTypeWithPrimaryKey = autoRepositoryAttr.RepositoryInterfaceWithPrimaryKey.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType);
                var genericImplType = autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.GetGenericArguments().Length == 2
                            ? autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(entityTypeInfo.EntityType, primaryKeyType)
                            : autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(entityTypeInfo.DeclaringType, entityTypeInfo.EntityType, primaryKeyType);

                builder.RegisterType(genericImplType).As(genericRepositoryTypeWithPrimaryKey);
            }
        }
    }
}
