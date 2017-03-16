using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Owin.Security.QQ.Provider
{
    public interface IQQAuthenticationProvider
    {
        Task Authenticated(QQAuthenticatedContext context);

        Task ReturnEndpoint(QQReturnEndpointContext context);

        Task Redirecting(QQRedirectingContext context);

        void ApplyRedirect(QQApplyRedirectContext context);
    }
}
