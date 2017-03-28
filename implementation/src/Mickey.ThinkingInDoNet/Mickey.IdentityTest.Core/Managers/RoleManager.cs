using Mickey.IdentityTest.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace Mickey.IdentityTest.Core.Managers
{
    public class RoleManager : RoleManager<Role>, IDisposable
    {
        public RoleManager(RoleStore<Role> store) : base(store)
        {
        }

        public static RoleManager Create(IdentityFactoryOptions<RoleManager> options, IOwinContext context)
        {
            var db = context.Get<Infrastructure.IdentityDbContext>();
            return new RoleManager(new RoleStore<Role>(db));
        }
    }
}
