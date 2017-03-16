using Mickey.EntityFramework;
using Mickey.Web.Test.Core.Models;
using System.Data.Entity;

namespace Mickey.Web.Test.Core.Stores
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<User> Users { get; set; }
    }
}