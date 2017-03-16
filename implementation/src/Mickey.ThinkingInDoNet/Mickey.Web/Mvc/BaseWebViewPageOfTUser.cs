using Mickey.Web.Extensions;
using Microsoft.AspNet.Identity;

namespace Mickey.Web.Mvc
{
    public abstract class BaseWebViewPage<TModel, TKey, TUser> : BaseWebViewPage<TModel>
        where TUser : class, IUser<TKey>
    {
        TUser m_User;

        public new TUser User
        {
            get
            {
                if (m_User == null)
                {

                    m_User = OwinContext.GetAppUser<TUser>();
                }
                return m_User;
            }
        }
    }
}
