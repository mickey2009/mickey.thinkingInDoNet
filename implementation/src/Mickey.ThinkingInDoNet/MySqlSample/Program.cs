using CodeFirstNewDatabaseSample;
using System.Data.Entity;

namespace MySqlSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BloggingDbContext>());
            var context = new BloggingDbContext();
            context.Blogs.Add(new CodeFirstModels.Blog
            {
                BlogId = 1,
                Name = "Entity Frame 链接 Mysql",
                Url = "http://blog.csdn.net/kmguo/article/details/19650299"
            });
            context.SaveChanges();
        }
    }
}
