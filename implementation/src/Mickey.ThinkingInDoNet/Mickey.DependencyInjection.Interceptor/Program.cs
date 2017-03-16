using Autofac;
using Autofac.Extras.DynamicProxy2;
using System;

namespace Mickey.DependencyInjection.Interceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>()
                   .As<ILogger>()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(CallLogger));

            builder.Register(c => new CallLogger(Console.Out));

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var logger = container.Resolve<ILogger>();
                logger.Log("hello interceptor");
            }

            Console.ReadLine();
        }
    }
}
