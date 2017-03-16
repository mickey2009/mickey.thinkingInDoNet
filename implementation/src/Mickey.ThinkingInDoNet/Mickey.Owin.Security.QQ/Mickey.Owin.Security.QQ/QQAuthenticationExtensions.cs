using Mickey.Owin.Security.QQ;
using Microsoft.Owin.Extensions;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owin
{
    public static class QQAuthenticationExtensions
    {
        public static IAppBuilder UseQQAuthentication(
            this IAppBuilder app,
            string appId,
            string appSecret)
        {
            var options = new QQAuthenticationOptions
                {
                    AppId = appId,
                    AppKey = appSecret
                };
            return UseQQAuthentication(app, options);
        }

        public static IAppBuilder UseQQAuthentication(this IAppBuilder app, QQAuthenticationOptions options)
        {
            if (app == null)
                throw new ArgumentNullException("app");
            if (options == null)
                throw new ArgumentNullException("options");

            app.Use(typeof(QQAuthenticationMiddleware), app, options);
            return app;
        }
    }
}
