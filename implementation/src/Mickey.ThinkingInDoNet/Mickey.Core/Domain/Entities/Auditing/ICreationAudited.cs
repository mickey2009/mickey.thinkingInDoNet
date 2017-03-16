using System;

namespace Mickey.Core.Domain.Entities.Auditing
{
    public interface ICreationAudited
    {
        DateTime? Created { get; set; }

        string CreatorId { get; set; }
    }
}
