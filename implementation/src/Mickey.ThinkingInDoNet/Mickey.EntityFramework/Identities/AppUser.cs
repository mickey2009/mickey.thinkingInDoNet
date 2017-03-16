using Mickey.Core.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mickey.EntityFramework.Identities
{
    public class AppUser : Entity<string>, IUser
    {
        public virtual string UserName { get; set; } = string.Empty;

        public virtual string PasswordHash { get; set; } = string.Empty;

        public virtual string Email { get; set; } = string.Empty;

        public bool EmailConfirmed { get; set; }

        public virtual string SecurityStamp { get; set; } = string.Empty;

        [Column(TypeName = "datetime2")]
        public virtual DateTime LockoutEndDateUtc { get; set; }

        public int AccessFailedCount { get; set; }
    }
}
