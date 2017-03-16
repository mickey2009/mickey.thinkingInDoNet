using Mickey.Core.Domain.Entities;
using Mickey.Core.Domian.Repositories;

namespace Mickey.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity> : EfRepositoryBase<TDbContext, TEntity, string>, IRepository<TEntity>
        where TEntity : class, IEntity<string>
        where TDbContext : IDbContext
    {
        public EfRepositoryBase(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}
