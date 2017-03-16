using Mickey.Core.ComponentModel;
using Mickey.Core.Infrastructure.Configuration;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Logging;
using Microsoft.Owin;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mickey.Web.Mvc
{
    public abstract class BaseController : Controller
    {
        IOwinContext m_OwinContext;
        IConfiguration m_Configuration;
        ILogger m_Logger;
        IErrorResultFactory m_ErrorResultFactory;

        public IOwinContext OwinContext
        {
            get
            {
                if (m_OwinContext == null)
                {
                    m_OwinContext = Request.GetOwinContext();
                }
                return m_OwinContext;
            }
            set
            {
                Requires.NotNull(value, "value");
                m_OwinContext = value;
            }
        }

        public IConfiguration Configuration
        {
            get
            {
                return m_Configuration;
            }
            set
            {
                Requires.NotNull(value, "value");
                m_Configuration = value;
            }
        }

        public ILogger Logger
        {
            get
            {
                return m_Logger;
            }
            set
            {
                Requires.NotNull(value, "value");
                m_Logger = value;
            }
        }

        public IErrorResultFactory ErrorResultFactory
        {
            get
            {
                return m_ErrorResultFactory;
            }
            set
            {
                Requires.NotNull(value, "value");
                m_ErrorResultFactory = value;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.LogError(string.Format("请求发生异常\r\nUrl:{0}\r\nController:{1}\r\nAction:{2}",
                filterContext.HttpContext.Request.RawUrl,
                filterContext.RouteData.GetRequiredString("controller"),
                filterContext.RouteData.GetRequiredString("action")),
                filterContext.Exception);

            if (!filterContext.ExceptionHandled && !WebEnvironment.IsDebuging)
            {
                var IsArgException = typeof(ArgumentException).IsAssignableFrom(filterContext.Exception.GetType());
                filterContext.Result = Error(IsArgException ? "参数错误" : "操作失败，请联系管理员。");
                filterContext.ExceptionHandled = true;
            }
        }

        protected virtual ActionResult Error(string format = null, params string[] args)
        {
            var message = string.IsNullOrWhiteSpace(format) ?
                Configuration.Get(Constants.ConfigKeys.DefaultErrorMessage, "操作失败，请稍后重试。") : format;
            return m_ErrorResultFactory.Create(message);
        }

        protected JsonResult Json(InvokedResult result, string contentType = "application/json")
        {
            Requires.NotNull(result, "result");
            var error = result.Error != null ? result.Error.Description : string.Empty;
            var data = new AjaxResponse<IReadOnlyCollection<ErrorDescriber>>(result.Succeeded, error, result.Errors);
            return Json(data, contentType, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult Json(bool succeeded, string message = null, object data = null, string contentType = "application/json")
        {
            return Json(new AjaxResponse<object>(succeeded, message, data), contentType, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected virtual Dictionary<string, string[]> ErrorToDictionary()
        {
            return ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
