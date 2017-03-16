using System;

namespace Mickey.Core.Domain.Entities.Auditing
{
    public class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited
    {
        public DateTime? Created { get; set; }

        public string CreatorId { get; set; }
    }
}
