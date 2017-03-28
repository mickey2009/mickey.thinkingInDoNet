using Mickey.IdentityTest.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Database = System.Data.Entity.Database;

namespace Mickey.IdentityTest.Core.Infrastructure
{
    public class IdentityDbContext : IdentityDbContext<User>
    {
        public IdentityDbContext() : base("IdentityDb")
        {
        }
    }
}
