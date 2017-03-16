using System;

namespace Mickey.Core.Domain.Entities.Auditing
{
    public class UpdationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IUpdationAudited
    {
        public DateTime? Updated { get; set; }

        public string UpdatedUserId { get; set; }
    }
}
