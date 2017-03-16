using Mickey.Core.ComponentModel;
using Mickey.Core.Infrastructure.Configuration;
using Microsoft.Framework.Configuration;
using System.Web.Mvc;

namespace Mickey.Web.Mvc
{
    public class DefaultErrorResultFactory : IErrorResultFactory
    {
        public static readonly string DefaultMessage = "操作失败，请稍后重试。";
        public static readonly string DefaultViewName = "Error";
        string m_DefaultErrorMessage;
        string m_DefaultErrorView;

        public DefaultErrorResultFactory(IConfiguration config)
        {
            Requires.NotNull(config, nameof(config));
            m_DefaultErrorMessage = config.Get(Constants.ConfigKeys.DefaultErrorMessage, DefaultMessage);
            m_DefaultErrorView = config.Get(Constants.ConfigKeys.DefaultErrorView, DefaultViewName);
        }

        public virtual ActionResult Create(string message)
        {
            return new ErrorResult(message ?? m_DefaultErrorMessage, m_DefaultErrorView);
        }
    }
}
