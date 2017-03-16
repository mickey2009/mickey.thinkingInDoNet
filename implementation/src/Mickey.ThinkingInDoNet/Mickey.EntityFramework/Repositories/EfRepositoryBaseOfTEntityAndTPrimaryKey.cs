using Mickey.Core.ComponentModel;
using Mickey.Core.Domain.Entities;
using Mickey.Core.Domian.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mickey.EntityFramework.Repositories
{
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IDisposable
       where TEntity : class, IEntity<TPrimaryKey>
       where TDbContext : IDbContext
    {
        public virtual TDbContext Context { get; }

        public virtual IDbSet<TEntity> DbSet { get; }

        public EfRepositoryBase(TDbContext dbContext)
        {
            Context = dbContext;
            DbSet = Context.Set<TEntity>();
        }

        #region Create

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            Requires.NotNull(entity, nameof(entity));
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Retrieval

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = DbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetAll(predicate).ToListAsync();
        }

        public async virtual Task<TEntity> FindAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
            if (entity == null)
                throw new NotSupportedException("无法找到实体");

            return entity;
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetAll().FirstOrDefaultAsync(predicate);
        }

        #endregion

        #region Update

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Requires.NotNull(entity, nameof(entity));
            DbSet.AddOrUpdate(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Delete

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Requires.NotNull(entity, nameof(entity));
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
            if (entity == null)
                return;

            await DeleteAsync(entity);
        }

        #endregion

        #region Aggregates

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetAll().CountAsync(predicate);
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return GetAll().LongCountAsync(predicate);
        }

        #endregion

        private static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(Expression.PropertyOrField(lambdaParam, "Id"), Expression.Constant(id, typeof(TPrimaryKey)));
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
