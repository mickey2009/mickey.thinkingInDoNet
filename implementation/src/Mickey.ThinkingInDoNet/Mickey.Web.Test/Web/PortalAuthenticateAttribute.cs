using Mickey.Core.ComponentModel;
using Mickey.Web.Extensions;
using Mickey.Web.Mvc;
using Mickey.Web.Test.Core.Managers;
using Mickey.Web.Test.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Logging;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc.Filters;

namespace Mickey.Web.Test.Web
{
    public class PortalAuthenticateAttribute : AuthenticateBaseAttribute, IAuthenticationFilter
    {
        private readonly bool m_HandleUnauthorizedRequest;

        public UserManager UserManager { get; set; }

        public PortalAuthenticateAttribute(bool handleUnauthorizedRequest = true)
        {
            m_HandleUnauthorizedRequest = handleUnauthorizedRequest;
        }

        protected override bool TryGetUser(IPrincipal principal, out IUser user)
        {
            user = null;
            try
            {
                var claimsPrincipal = (ClaimsPrincipal)principal;
                var id = claimsPrincipal.GetClaimTypeValue(ClaimTypes.NameIdentifier);
                user = UserManager.FindByIdFromCache(id);
                return user != null;
            }
            catch (ClaimNotFoundException ex)
            {
                Logger.LogWarning("创建User时发生错误", ex);
                return false;
            }
        }

        protected override void OnAuthenticated(AuthenticationContext filterContext, IUser user)
        {
            Requires.NotNull(filterContext, nameof(filterContext));
            Requires.NotNull(user, nameof(user));

            var userTemp = (User)user;
            var owinContext = filterContext.HttpContext.GetOwinContext();
            owinContext.SetAppUser(userTemp);
        }

        protected override void HandleUnauthorizedRequest(AuthenticationContext filterContext)
        {
            if (m_HandleUnauthorizedRequest)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}