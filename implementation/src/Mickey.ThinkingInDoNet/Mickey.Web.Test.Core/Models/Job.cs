using Mickey.Core.ComponentModel;
using Mickey.Core.Domain.Entities.Auditing;

namespace Mickey.Web.Test.Core.Models
{
    public class Job : AuditedEntity
    {
        public Job()
        {
            Id = SequentialGuid.NewGuidString();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public virtual User Creator { get; set; }
    }
}