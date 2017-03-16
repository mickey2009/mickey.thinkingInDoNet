using Mickey.Web.Test.Core.Services;
using Mickey.Web.Test.ViewModels;
using Mickey.Web.Test.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mickey.Web.Test.Controllers
{
    public class JobController : PortalController
    {
        private JobService m_JobService;

        public JobController(JobService jobService)
        {
            m_JobService = jobService;
        }

        public async Task<ActionResult> Index()
        {
            var jobs = await m_JobService.GetAsync();
            return View(jobs.Select(j => new JobViewModel(j)));
        }
    }
}