using Mickey.Core.ComponentModel;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Mickey.EntityFramework.Identities
{
    public class UserStore<TUser> :
           IUserStore<TUser>,
           IUserPasswordStore<TUser>,
           IUserEmailStore<TUser>,
           IUserSecurityStampStore<TUser>,
           IUserLockoutStore<TUser, string>,
           IQueryableUserStore<TUser>,
           IUserTwoFactorStore<TUser, string>,
           IDisposable
           where TUser : AppUser
    {
        private IDbContext DbContext { get; set; }

        #region IUserStore
        public UserStore(IDbContext dbContext)
        {
            Requires.NotNull(dbContext, "dbContext");
            DbContext = dbContext;
        }

        public Task CreateAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            DbContext.Set<TUser>().Add(user);
            return DbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            DbContext.Set<TUser>().Remove(user);
            return DbContext.SaveChangesAsync();
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            Requires.NotNullOrEmpty(userId, "userId");
            return DbContext.Set<TUser>().SingleOrDefaultAsync(u => u.Id == userId);
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            Requires.NotNull(userName, "userName");
            return DbContext.Set<TUser>().SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public Task UpdateAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            ((DbContext)DbContext).Entry(user).State = EntityState.Modified;
            return DbContext.SaveChangesAsync();
        }
        #endregion

        public void Dispose()
        {
            DbContext.Dispose();
        }

        #region IUserPasswordStore
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            Requires.NotNull(user, "user");
            Requires.NotNull(passwordHash, "passwordHash");
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.PasswordHash == null);
        }
        #endregion

        #region IUserEmailStore
        public Task SetEmailAsync(TUser user, string email)
        {
            Requires.NotNull(user, "user");
            Requires.NotNull(email, "email");
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            Requires.NotNull(user, "user");
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            Requires.NotNull(email, "email");
            return DbContext.Set<TUser>().SingleOrDefaultAsync(u => u.Email == email);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            Requires.NotNull(user, "user");
            Requires.NotNullOrEmpty(stamp, "stamp");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.SecurityStamp);
        }
        #endregion

        #region IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.LockoutEndDateUtc != default(DateTime)
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            Requires.NotNull(user, "user");
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? default(DateTime) : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(true); //默认所有账户都支持锁定。
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            Requires.NotNull(user, "user");
            return Task.FromResult(0); //默认所有账户都支持锁定。
        }
        #endregion

        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            return Task.FromResult(false);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }

        public IQueryable<TUser> Users
        {
            get
            {
                return DbContext.Set<TUser>().AsQueryable();
            }
        }
    }
}
