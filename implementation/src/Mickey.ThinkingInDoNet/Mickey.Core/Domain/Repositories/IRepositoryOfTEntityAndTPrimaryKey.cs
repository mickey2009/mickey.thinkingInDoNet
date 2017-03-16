using Mickey.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mickey.Core.Domian.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Create

        Task<TEntity> CreateAsync(TEntity entity);

        #endregion

        #region Retrieval

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> FindAsync(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Update

        Task<TEntity> UpdateAsync(TEntity entity);

        #endregion

        #region Delete

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(TPrimaryKey id);

        #endregion

        #region Aggregates

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null);

        #endregion
    }
}
