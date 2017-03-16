using Mickey.Core.Domain.Entities;

namespace Mickey.Core.Domian.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string>
         where TEntity : class, IEntity<string>
    {
    }
}
