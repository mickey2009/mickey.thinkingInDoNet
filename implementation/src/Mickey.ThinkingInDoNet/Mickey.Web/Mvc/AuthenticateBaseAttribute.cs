using Mickey.Web.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Logging;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Mickey.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class AuthenticateBaseAttribute : FilterAttribute, IAuthenticationFilter
    {
        static readonly Type _AllowAnonymousType = typeof(AllowAnonymousAttribute);

        public ILogger Logger { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(_AllowAnonymousType, inherit: true);
            if (skipAuthorization)
                return;

            IUser user;
            var principal = filterContext.Principal;
            if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated || !TryGetUser(principal, out user))
            {
                HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                OnAuthenticated(filterContext, user);
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }

        protected abstract bool TryGetUser(IPrincipal principal, out IUser user);

        protected virtual void HandleUnauthorizedRequest(AuthenticationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        protected virtual void OnAuthenticated(AuthenticationContext filterContext, IUser user)
        {
            filterContext.HttpContext.GetOwinContext().SetAppUser(user);
        }
    }
}
