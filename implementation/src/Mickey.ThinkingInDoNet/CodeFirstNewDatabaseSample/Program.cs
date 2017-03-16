using CodeFirstModels;
using System;
using System.Collections.Generic;

namespace CodeFirstNewDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new BloggingDbContext())
            {
                var blog = new Blog()
                {
                    Name = "Design Parttens",
                    Posts = new List<Post>()
                    {
                        new Post() {  Content = "balabala" ,  Title = "Method Factory"}
                    }
                };
                dbContext.Blogs.Add(blog);
                dbContext.SaveChanges();

                foreach (var b in dbContext.Blogs)
                {
                    Console.WriteLine(b.Name);
                }
                Console.ReadLine();
            }
        }
    }
}
