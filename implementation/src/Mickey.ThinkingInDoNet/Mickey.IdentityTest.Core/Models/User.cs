using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.IdentityTest.Core.Models
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
    }
}
