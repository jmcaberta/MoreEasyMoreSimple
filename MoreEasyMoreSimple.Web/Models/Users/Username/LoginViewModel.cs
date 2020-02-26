

using System.ComponentModel.DataAnnotations;

namespace MoreEasyMoreSimple.Web.Models.Users.Username
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
