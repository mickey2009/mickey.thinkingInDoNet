using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public class QQAuthenticationProvider : IQQAuthenticationProvider
    {
        public QQAuthenticationProvider()
        {
            OnAuthenticated = context => Task.FromResult<object>(null);
            OnReturnEndpoint = context => Task.FromResult<object>(null);
            OnApplyRedirect = context =>
                context.Response.Redirect(context.RedirectUri);
            OnRedirecting = context => Task.FromResult<object>(null);
        }

        public Func<QQAuthenticatedContext, Task> OnAuthenticated { get; set; }

        public Func<QQReturnEndpointContext, Task> OnReturnEndpoint { get; set; }

        public Func<QQRedirectingContext, Task> OnRedirecting { get; set; }

        public Action<QQApplyRedirectContext> OnApplyRedirect { get; set; }

        public virtual Task Authenticated(QQAuthenticatedContext context)
        {
            return OnAuthenticated(context);
        }

        public virtual Task ReturnEndpoint(QQReturnEndpointContext context)
        {
            return OnReturnEndpoint(context);
        }

        public virtual Task Redirecting(QQRedirectingContext context)
        {
            return OnRedirecting(context);
        }

        public virtual void ApplyRedirect(QQApplyRedirectContext context)
        {
            OnApplyRedirect(context);
        }
    }
}
