using CodeFirstModels;
using System.Data.Entity;

namespace CodeFirstNewDatabaseSample
{
    public class BloggingDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
