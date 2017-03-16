using Mickey.Core.Domian.Repositories;
using System;

namespace Mickey.EntityFramework.Repositories
{
    internal class AutoRepositoryTypes
    {
        public static AutoRepositoryTypes Default { get; private set; }

        public Type RepositoryInterface { get; private set; }

        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        public Type RepositoryImplementation { get; private set; }

        public Type RepositoryImplementationWithPrimaryKey { get; private set; }

        static AutoRepositoryTypes()
        {
            Default = new AutoRepositoryTypes(
                typeof(IRepository<>),
                typeof(IRepository<,>),
                typeof(EfRepositoryBase<,>),
                typeof(EfRepositoryBase<,,>)
                );
        }

        public AutoRepositoryTypes(
                Type repositoryInterface,
                Type repositoryInterfaceWithPrimaryKey,
                Type repositoryImplementation,
                Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}
