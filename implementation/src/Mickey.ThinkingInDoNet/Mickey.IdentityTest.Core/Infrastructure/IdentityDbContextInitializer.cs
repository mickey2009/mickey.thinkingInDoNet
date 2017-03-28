using Mickey.IdentityTest.Core.Managers;
using Mickey.IdentityTest.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Mickey.IdentityTest.Core.Infrastructure
{
    public class IdentityDbContextInitializer : DropCreateDatabaseIfModelChanges<IdentityDbContext>
    {
        protected override void Seed(IdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(IdentityDbContext context)
        {
            var roleManager = new RoleManager(new RoleStore<Role>(context));
            roleManager.Create(new Role("Admin") { });
            roleManager.Create(new Role("User") { });

            var userManager = new UserManager(new UserStore<User>(context));
            var password = "1234abcd";
            var mickey = new User { UserName = "Mickey" };
            var lily = new User { UserName = "Lily" };
            userManager.Create(mickey, password);
            userManager.Create(lily, password);

            userManager.AddToRole(mickey.Id, "Admin");
            userManager.AddToRole(lily.Id, "User");
        }
    }
}
