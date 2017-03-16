using Mickey.EntityFramework.Tests.Models;
using System.Collections.Generic;

namespace Mickey.EntityFramework.Tests
{
    public class BloggingDbContextInitializer : System.Data.Entity.DropCreateDatabaseAlways<BloggingDbContext>
    {
        protected override void Seed(BloggingDbContext dbContext)
        {
            dbContext.Blogs.Add(new Blog()
            {
                Name = "Design Parttens",
                Posts = new List<Post>()
                    {
                        new Post() {  Content = "balabala" ,  Title = "Method Factory"},
                        new Post() {  Content = "balabala" ,  Title = "Strategy"}
                    }
            });
            dbContext.Blogs.Add(new Blog()
            {
                Name = "Refactor",
                Posts = new List<Post>()
                    {
                        new Post() {  Content = "balabala" ,  Title = "Method Move"},
                    }
            });
            dbContext.SaveChanges();
        }
    }
}
