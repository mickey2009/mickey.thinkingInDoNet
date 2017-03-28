using Mickey.IdentityTest.Core.Managers;
using Mickey.IdentityTest.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Mickey.IdentityTest.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(_UserManager.Users);
        }
    }
}