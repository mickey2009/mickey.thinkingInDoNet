using Mickey.Core.ComponentModel;
using Mickey.Core.Tasks;
using Mickey.EntityFramework.Identities;
using Mickey.Web.Test.Core.Models;
using Mickey.Web.Test.Core.Stores;
using System;
using System.Runtime.Caching;

namespace Mickey.Web.Test.Core.Managers
{
    public class UserManager : UserManager<User>
    {
        public UserManager(UserStore store) : base(store)
        {
        }

        static MemoryCache _UserCache = new MemoryCache("UserManager.UserCache");

        public User FindByIdFromCache(string userId)
        {
            Requires.NotNull(userId, nameof(userId));

            var cacheKey = userId;
            var user = _UserCache[cacheKey] as User;
            if (user == null)
            {
                user = AsyncHelper.RunSync(() => FindByIdAsync(userId));
                if (user != null)
                {
                    _UserCache.Set(cacheKey, user, DateTimeOffset.Now.AddMinutes(20));
                }
            }
            return user;
        }
    }
}
