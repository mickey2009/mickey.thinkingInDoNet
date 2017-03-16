using System;

namespace Mickey.Core.Domain.Entities.Auditing
{
    public interface IUpdationAudited
    {
        DateTime? Updated { get; set; }

        string UpdatedUserId { get; set; }
    }
}
