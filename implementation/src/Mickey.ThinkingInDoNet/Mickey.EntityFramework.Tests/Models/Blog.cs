using Mickey.Core.Domain.Entities;
using System.Collections.Generic;

namespace Mickey.EntityFramework.Tests.Models
{
    public class Blog : Entity<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
