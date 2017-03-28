using Mickey.IdentityTest.Web;
using System.Web.Mvc;

namespace Mickey.IdentityTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}