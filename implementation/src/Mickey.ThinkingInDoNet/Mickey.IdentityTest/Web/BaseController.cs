using Mickey.IdentityTest.Core.Managers;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mickey.IdentityTest.Web
{
    public class BaseController : Controller
    {
        //通过_UserManager可以实现对用户数据进行CRUD
        protected UserManager _UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
        }
    }
}