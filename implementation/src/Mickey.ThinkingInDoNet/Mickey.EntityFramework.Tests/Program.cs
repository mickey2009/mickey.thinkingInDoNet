using Autofac;
using Mickey.Core.Domian.Repositories;
using Mickey.EntityFramework.Repositories;
using Mickey.EntityFramework.Tests.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Mickey.EntityFramework.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new BloggingDbContextInitializer());

            var builder = new ContainerBuilder();
            AutoRepositoryRegistrar.RegisterForDbContext(typeof(BloggingDbContext), builder);
            builder.RegisterType<BloggingDbContext>().AsSelf();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var blogRepository = container.Resolve<IRepository<Blog, int>>();
                var blogs = blogRepository.GetAll().ToList();
                Print(blogs);
            }

            Console.WriteLine();
        }

        public static void Print(IEnumerable<Blog> blogs)
        {
            foreach (var blog in blogs)
            {
                if (blog.Posts != null)
                {
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($"Blog Id: {blog.Id} ,  Blog Name:{blog.Name} , Post Title:{post.Title} , Post Content {post.Content}");
                    }
                }
                else
                {
                    Console.WriteLine($"Blog Id: {blog.Id} ,  Blog Name:{blog.Name}");
                }
            }
        }
    }
}
