using Mickey.Core.Application;
using Mickey.Core.Domian.Repositories;
using Mickey.Web.Test.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mickey.Web.Test.Core.Services
{
    public class JobService : ApplicationService
    {
        private IRepository<Job> m_JobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            m_JobRepository = jobRepository;
        }

        public virtual async Task<IEnumerable<Job>> GetAsync()
        {
            return await m_JobRepository.GetAllListAsync();
        }
    }
}
