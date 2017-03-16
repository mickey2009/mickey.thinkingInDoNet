using Autofac;
using Autofac.Integration.Owin;
using Mickey.Core.Infrastructure.DependencyInjection;
using System.Web;

namespace Mickey.Web.DependencyInjection
{
    public class OwinContextLifetimeScopeProvider : IIocContainerProvider
    {
        ILifetimeScope m_Container;

        public ILifetimeScope Current
        {
            get
            {
                if (m_Container == null)
                {
                    m_Container = HttpContext.Current.Request.GetOwinContext().GetAutofacLifetimeScope();
                }
                return m_Container;
            }
        }
    }
}
