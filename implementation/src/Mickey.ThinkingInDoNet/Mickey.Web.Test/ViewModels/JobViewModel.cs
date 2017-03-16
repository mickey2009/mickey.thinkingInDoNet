using Mickey.Web.Test.Core.Models;

namespace Mickey.Web.Test.ViewModels
{
    public class JobViewModel
    {
        public JobViewModel(Job job)
        {
            Name = job.Name;
            Description = job.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}