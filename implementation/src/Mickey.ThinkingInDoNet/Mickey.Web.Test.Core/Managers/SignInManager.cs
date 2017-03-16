using Mickey.Web.Test.Core.Models;
using Microsoft.Owin.Security;

namespace Mickey.Web.Test.Core.Managers
{
    public class SignInManager : Microsoft.AspNet.Identity.Owin.SignInManager<User, string>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
}
