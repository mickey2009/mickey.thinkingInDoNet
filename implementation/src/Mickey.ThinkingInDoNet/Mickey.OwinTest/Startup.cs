using Mickey.OwinTest.Middlewares;
using Owin;

namespace Mickey.OwinTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<LoggerMiddleware>();
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("<p>Hello World!</p>");
            });
        }
    }
}