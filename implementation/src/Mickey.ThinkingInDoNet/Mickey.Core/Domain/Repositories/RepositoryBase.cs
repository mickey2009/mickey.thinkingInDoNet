using Mickey.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mickey.Core.Domian.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IDisposable
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public abstract Task<TEntity> CreateAsync(TEntity entity);

        public abstract IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        public abstract Task<TEntity> UpdateAsync(TEntity entity);

        public abstract Task DeleteAsync(TEntity entity);

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Task.FromResult(GetAll(predicate).ToList());
        }

        public virtual async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
            if (entity == null)
                throw new NotSupportedException("无法找到实体");

            return entity;
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Task.FromResult(GetAll(predicate).FirstOrDefault());
        }

        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
            if (entity == null)
                return;

            await DeleteAsync(entity);
            return;
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Task.FromResult(GetAll(predicate).Count());
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return Task.FromResult(GetAll(predicate).LongCount());
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public void Dispose()
        {
        }
    }
}
