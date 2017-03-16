using Microsoft.Owin;
using System.Threading.Tasks;

namespace Mickey.OwinTest.Middlewares
{
    public class LoggerMiddleware : OwinMiddleware
    {
        public LoggerMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await context.Response.WriteAsync("<p>LoggerMiddleware begin</p>");
            await this.Next.Invoke(context);
            await context.Response.WriteAsync("<p>LoggerMiddleware end</p>");
        }
    }
}