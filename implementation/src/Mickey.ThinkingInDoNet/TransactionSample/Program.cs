using CodeFirstExistingDatabaseSample;
using CodeFirstModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TransactionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new BloggingDbContextInitializer());

            Console.WriteLine("SingleDatabaseTransactionTest");
            SingleDatabaseTransactionTest();

            Console.WriteLine("MSDTCTransactionTest");
            MSDTCTransactionTest();
        }

         static void Print(IEnumerable<Blog> blogs)
        {
            foreach (var blog in blogs)
            {
                Console.WriteLine($"Blog Id: {blog.BlogId} ,  Blog Name:{blog.Name}");
            }
            Console.WriteLine();
        }

        static void SingleDatabaseTransactionTest()
        {
            using (var dbContext = new BloggingDbContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    AppendBlogs(dbContext);
                    transaction.Commit();
                }
                Print(dbContext.Blogs);
                Console.ReadLine();
            }
        }

        static void MSDTCTransactionTest()
        {
            using (var dbContext = new BloggingDbContext())
            {
                using (var transaction =  new TransactionScope())
                {
                    AppendBlogs(dbContext);
                    transaction.Complete();
                }
                Print(dbContext.Blogs);
                Console.ReadLine();
            }
        }

        static void AppendBlogs(BloggingDbContext dbContext )
        {
            dbContext.Blogs.Add(new Blog
            {
                Name = "test1"
            });
            dbContext.SaveChanges();
            dbContext.Blogs.Add(new Blog
            {
                Name = "test2"
            });
            dbContext.SaveChanges();
        }
    }
}
