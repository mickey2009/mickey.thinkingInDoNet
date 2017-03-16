using Mickey.Core.Common;
using Mickey.Core.ComponentModel;
using Microsoft.AspNet.Identity;
using System;

namespace Mickey.EntityFramework.Identities
{
    public class UserManager<TUser> : Microsoft.AspNet.Identity.UserManager<TUser> where TUser : AppUser
    {
        public UserManager(UserStore<TUser> store) : base(store)
        {
            Requires.NotNull(store, nameof(store));

            UserValidator = new UserValidator<TUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator(GlobalRegularPattern.SimplyPassword);
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(30);
            MaxFailedAccessAttemptsBeforeLockout = 5;
        }
    }
}
