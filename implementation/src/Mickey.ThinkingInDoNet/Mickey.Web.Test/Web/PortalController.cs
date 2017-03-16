using Mickey.Web.Mvc;
using Mickey.Web.Test.Core.Models;

namespace Mickey.Web.Test.Web
{
    [PortalAuthenticate]
    public class PortalController : BaseController<User>
    {
    }
}