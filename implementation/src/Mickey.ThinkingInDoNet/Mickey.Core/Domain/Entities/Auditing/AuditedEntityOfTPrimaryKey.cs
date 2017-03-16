using System;

namespace Mickey.Core.Domain.Entities.Auditing
{
    public class AuditedEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IAudited
    {
        public DateTime? Updated { get; set; }

        public string UpdatedUserId { get; set; }
    }
}
