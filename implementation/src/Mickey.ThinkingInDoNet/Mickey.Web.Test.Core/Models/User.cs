using Mickey.Core.ComponentModel;
using Mickey.EntityFramework.Identities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mickey.Web.Test.Core.Models
{
    public class User : AppUser
    {
        public User()
        {
            Id = SequentialGuid.NewGuidString();
            Created = DateTimeService.Now;
        }

        public string Nickname { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime Created { get; set; }
    }
}