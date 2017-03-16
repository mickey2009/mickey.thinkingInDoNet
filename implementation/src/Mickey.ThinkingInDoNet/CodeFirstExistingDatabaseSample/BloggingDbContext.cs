using CodeFirstModels;
using System.Data.Entity;

namespace CodeFirstExistingDatabaseSample
{
    public class BloggingDbContext : DbContext
    {
        public BloggingDbContext() : base("name=BloggingContext")
        {
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
