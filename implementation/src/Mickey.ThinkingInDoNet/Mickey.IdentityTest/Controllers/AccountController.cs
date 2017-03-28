using Mickey.IdentityTest.Core.Managers;
using Mickey.IdentityTest.Models;
using Mickey.IdentityTest.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mickey.IdentityTest.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindAsync(model.Name, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else {
                    ClaimsIdentity ident = await _UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _AuthManager.SignOut();
                    _AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/Home" : returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            _AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager _AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}