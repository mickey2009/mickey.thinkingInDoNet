using Mickey.Web.Test.Core.Services;
using Mickey.Web.Test.Web;
using System.Web.Mvc;

namespace Mickey.Web.Test.Controllers
{
    public class HomeController : PortalController
    {
        private IAppSession m_appSession;

        public HomeController(IAppSession appSession)
        {
            m_appSession = appSession;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}