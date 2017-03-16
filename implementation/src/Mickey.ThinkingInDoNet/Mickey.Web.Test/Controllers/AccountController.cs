using Mickey.Web.Test.Core.Managers;
using Mickey.Web.Test.ViewModels;
using Mickey.Web.Test.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mickey.Web.Test.Controllers
{
    public class AccountController : AnonymousController
    {
        private UserManager m_UserManager;

        public AccountController(UserManager userManager)
        {
            m_UserManager = userManager;
        }

        [Route("Login"), HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginRequest { ReturnUrl = returnUrl });
        }

        [Route("Login"), HttpPost]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var signManager = new SignInManager(m_UserManager, HttpContext.GetOwinContext().Authentication);
                var user = await m_UserManager.FindByNameAsync(request.UserName);
                var result = await signManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
                if (result == SignInStatus.Success)
                {
                    return Redirect(request.ReturnUrl);
                }
                else if (result == SignInStatus.LockedOut)
                {
                    ModelState.AddModelError("", "用户被锁定");
                }
                else
                {
                    ModelState.AddModelError("", "用户名或者密码错误");
                }
            }
            return View(request);
        }

        [Route("Logout"), HttpPost]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("~/");
        }
    }
}