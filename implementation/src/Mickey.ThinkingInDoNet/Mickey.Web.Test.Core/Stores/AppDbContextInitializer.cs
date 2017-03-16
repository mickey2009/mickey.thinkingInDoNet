using Mickey.Web.Test.Core.Managers;
using Mickey.Web.Test.Core.Models;
using Microsoft.AspNet.Identity;

namespace Mickey.Web.Test.Core.Stores
{
    public class AppDbContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext dbContext)
        {
            var userStore = new UserStore(dbContext);
            var userManager = new UserManager(userStore);
            var password = "1234abcd";
            var admin = CreateUser("admin");
            var result = userManager.Create(admin, password);
            userManager.Create(CreateUser("user"), password);

            var jobDbSet = dbContext.Set<Job>();
            jobDbSet.Add(CreateJob("程序猿鼓励师", admin.Id));
            jobDbSet.Add(CreateJob("开发Leader", admin.Id));
            dbContext.SaveChanges();
        }

        Job CreateJob(string jobName, string creatorId)
        {
            return new Job
            {
                Name = jobName,
                CreatorId = creatorId
            };
        }

        User CreateUser(string userName)
        {
            return new User
            {
                UserName = userName,
                Nickname = userName,
                Email = $"{userName}@163.com"
            };
        }
    }
}