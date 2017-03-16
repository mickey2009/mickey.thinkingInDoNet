using System.Web;
using System.Web.Mvc;

namespace Mickey.OAuth2.MvcAuth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
