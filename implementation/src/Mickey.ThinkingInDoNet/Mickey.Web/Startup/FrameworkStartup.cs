using Autofac;
using Mickey.Core.Infrastructure.Configuration;
using Mickey.Core.Infrastructure.Logging;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Logging;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[assembly: OwinStartup(typeof(Mickey.Web.Startup.FrameworkStartup), "PostStart")]
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mickey.Web.Startup.FrameworkStartup), "PreStart")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Mickey.Web.Startup.FrameworkStartup), "Shutdown")]

namespace Mickey.Web.Startup
{
    public static class FrameworkStartup
    {
        static StartupContext _Context;
        static IStartup _Startup;
        static ContainerBuilder _Builder = new ContainerBuilder();
        static IContainer _Container;

        public static void PreStart()
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EnterpriseLibraryLoggerProvider());
            var logger = loggerFactory.CreateLogger("Startup");

            _Startup = LoadStartupInstance(logger);
            _Context = new StartupContext { Logger = logger };

            _Startup.Initialize(_Context);

            var config = GetConfiguration(_Context);
            _Context.SetConfiguration(config);
            AppConfiguration.Set(config);

            var appLoger = loggerFactory.CreateLogger(config.GetRequiredItem(Constants.ConfigKeys.AppName));
            _Builder.RegisterInstance(appLoger).As<ILogger>();
            _Builder.RegisterInstance(config).As<IConfiguration>();
            _Builder.RegisterInstance(loggerFactory).As<ILoggerFactory>();

            _Startup.PreStart(_Context, _Builder);
            _Container = _Builder.Build();
        }

        public static void PostStart(IAppBuilder app)
        {
            ConfigureOwin(_Context.Configuration, app);
            _Startup.PostStart(_Context, app, _Container);
        }

        static IStartup LoadStartupInstance(ILogger logger)
        {
            var startups = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    IEnumerable<Type> types;

                    try
                    {
                        types = a.GetTypes();
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        logger.LogWarning("搜索IStartup时发生异常", e);
                        Array.ForEach(e.LoaderExceptions, le => logger.LogWarning("LoaderException", le));
                        types = e.Types.Where(t => t != null).Select(t => t.GetTypeInfo());
                    }
                    return types.Where(t => t.IsClass && !t.IsAbstract && !t.IsValueType && t.IsVisible
                        && typeof(IStartup).IsAssignableFrom(t));
                }).ToList();
            if (startups.Count == 1)
                return Activator.CreateInstance(startups[0]) as IStartup;

            var ex = new InvalidOperationException(startups.Count == 0 ? "找不到IStartup" : "IStartup的实现类不能超过两个");
            throw ex;
        }

        static IConfiguration GetConfiguration(StartupContext context)
        {
            if (context.Configuration != null)
                return context.Configuration;

            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddAppSettings();
            var config = configBuilder.Build();
            return config;
        }

        static void ConfigureOwin(IConfiguration config, IAppBuilder app)
        {
            app.UseAutofacMiddleware(_Container);
        }

        public static void Shutdown()
        {
            _Startup.Shutdown(_Context);
        }
    }
}
