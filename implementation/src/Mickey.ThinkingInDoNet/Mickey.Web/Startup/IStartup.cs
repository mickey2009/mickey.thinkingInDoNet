using Autofac;
using Owin;

namespace Mickey.Web.Startup
{
    /// <summary>
    /// 定义了需要在应用程序启动时执行自定义操作的一些方法。
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// 初始化配置等。
        /// </summary>
        /// <param name="context"></param>
        void Initialize(StartupContext context);

        /// <summary>
        /// 定义在应用程序启动之前的操作。
        /// </summary>
        void PreStart(StartupContext context, ContainerBuilder builder);

        /// <summary>
        /// 定义在应用程序启动之后的操作。
        /// </summary>
        void PostStart(StartupContext context, IAppBuilder app, IContainer container);

        /// <summary>
        /// 定义应用程序卸载时的操作。
        /// </summary>
        void Shutdown(StartupContext context);
    }
}
