using System.ComponentModel.DataAnnotations;

namespace Mickey.Web.Test.ViewModels
{
    public class LoginRequest
    {
        [Display(Name = "用户名"), Required(ErrorMessage = "请输入{0}")]
        public string UserName { get; set; }

        [Display(Name = "密码"), Required(ErrorMessage = "请输入{0}")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}