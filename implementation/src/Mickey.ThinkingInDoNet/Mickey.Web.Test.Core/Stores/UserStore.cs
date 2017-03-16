using Mickey.EntityFramework;
using Mickey.EntityFramework.Identities;
using Mickey.Web.Test.Core.Models;

namespace Mickey.Web.Test.Core.Stores
{
    public class UserStore : UserStore<User>
    {
        public UserStore(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}