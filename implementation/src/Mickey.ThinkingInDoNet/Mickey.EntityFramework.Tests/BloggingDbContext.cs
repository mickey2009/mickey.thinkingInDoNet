using Mickey.EntityFramework.Tests.Models;
using System.Data.Entity;

namespace Mickey.EntityFramework.Tests
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
