using System.Web.Mvc;

namespace Mickey.Web.Mvc
{
    public interface IErrorResultFactory
    {
        ActionResult Create(string message);
    }
}
