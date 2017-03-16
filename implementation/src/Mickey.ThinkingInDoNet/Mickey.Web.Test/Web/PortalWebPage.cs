using Mickey.Web.Mvc;
using Mickey.Web.Test.Core.Models;

namespace Mickey.Web.Test.Web
{
    public abstract class PortalWebPage<TModel> : BaseWebViewPage<TModel, string, User>
    {
    }
}