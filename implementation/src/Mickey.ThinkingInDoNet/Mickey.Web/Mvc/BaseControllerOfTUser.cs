using Mickey.Web.Extensions;
using System;

namespace Mickey.Web.Mvc
{
    public class BaseController<TUser> : BaseController
        where TUser : class
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
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                OwinContext.SetAppUser(value);
                m_User = value;
            }
        }
    }
}
