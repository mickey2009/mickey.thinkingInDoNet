using Mickey.IdentityTest.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Mickey.IdentityTest.Controllers
{
    public class ClaimsController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
            if (ident == null)
            {
                return View("Error", new string[] { "No claims available" });
            }
            else
            {
                return View(ident.Claims);
            }
        }
    }
}