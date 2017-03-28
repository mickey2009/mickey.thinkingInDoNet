using Mickey.IdentityTest.Core.Infrastructure;
using Mickey.IdentityTest.Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.IdentityTest.Core.Managers
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store) : base(store)
        {
        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            var db = context.Get<Infrastructure.IdentityDbContext>();
            return new UserManager(new UserStore<User>(db));
        }
    }
}
