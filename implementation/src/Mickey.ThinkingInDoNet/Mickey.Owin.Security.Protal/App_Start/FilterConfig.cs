using System.Web;
using System.Web.Mvc;

namespace Mickey.Owin.Security.Protal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
