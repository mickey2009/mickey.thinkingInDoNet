using Mickey.Web.Test.Core.Models;

namespace Mickey.Web.Test.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
        }

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}