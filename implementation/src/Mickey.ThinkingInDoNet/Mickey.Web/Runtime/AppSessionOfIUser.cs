using Mickey.Web.Extensions;
using System.Web;

namespace Mickey.Web.Runtime
{
    public class AppSession<IUser> : IAppSession<IUser>
             where IUser : class, Microsoft.AspNet.Identity.IUser
    {
        IUser m_User;

        public IUser User
        {
            get
            {
                if (m_User == null)
                {
                    m_User = HttpContext.Current.Request.GetOwinContext().GetAppUser<IUser>();
                }
                return m_User;
            }
        }
    }
}
