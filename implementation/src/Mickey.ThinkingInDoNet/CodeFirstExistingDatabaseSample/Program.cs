using CodeFirstModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace CodeFirstExistingDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new BloggingDbContextInitializer());

            Console.WriteLine("EntityStateTest");
            EntityStateTest();

            Console.WriteLine("PrintContextInfoByLazyLoading");
            PrintContextInfoByLazyLoading();

            Console.WriteLine("PrintContextInfoByEagerly");
            PrintContextInfoByEagerlyLoading();

            Console.WriteLine("LoalDataTest");
            LoalDataTest();

            Console.ReadLine();
        }

        /// <summary>
        /// 验证Lazy Loading
        /// </summary>
        public static void PrintContextInfoByLazyLoading()
        {
            using (var dbContext = new BloggingDbContext())
            {
                Print(dbContext.Blogs);
            }
        }

        /// <summary>
        /// 验证Eagerly Loading. 
        /// </summary>
        public static void PrintContextInfoByEagerlyLoading()
        {
            using (var dbContext = new BloggingDbContext())
            {
                var query = dbContext.Set<Blog>().AsQueryable().Include(b => b.Posts);
                Print(query);
            }
        }

        /// <summary>
        /// 验证Loal Data
        /// </summary>
        public static void LoalDataTest()
        {
            using (var dbContext = new BloggingDbContext())
            {
                dbContext.Blogs.Load();
                dbContext.Blogs.Add(new Blog { Name = "Domain Drive Design" });
                dbContext.Blogs.Remove(dbContext.Blogs.First());
                Print(dbContext.Blogs.Local);
            }
        }

        /// <summary>
        /// 验证Entity State
        /// </summary>
        public static void EntityStateTest()
        {
            using (var dbContext = new BloggingDbContext())
            {
                var blog = dbContext.Blogs.FirstOrDefault();
                blog.Name = blog.Name + "New1";

                Console.WriteLine($"Entity State: {dbContext.Entry(blog).State}");
                dbContext.SaveChanges();
                Console.WriteLine($"Entity State: {dbContext.Entry(blog).State}");

                Print(dbContext.Set<Blog>().ToList());
            }
        }

        public static void Print(IEnumerable<Blog> blogs)
        {
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.GetType().ToString());
                Console.WriteLine(ObjectContext.GetObjectType(blog.GetType()).ToString());
                if (blog.Posts != null)
                {
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($"Blog Id: {blog.BlogId} ,  Blog Name:{blog.Name} , Post Title:{post.Title} , Post Content {post.Content}");
                    }
                }
                else
                {
                    Console.WriteLine($"Blog Id: {blog.BlogId} ,  Blog Name:{blog.Name}");
                }
            }
            Console.WriteLine();
        }
    }
}
