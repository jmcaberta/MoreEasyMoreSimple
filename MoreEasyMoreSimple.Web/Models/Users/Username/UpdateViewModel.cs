
using System.ComponentModel.DataAnnotations;

namespace MoreEasyMoreSimple.Web.Models.Users.Username
{
    public class UpdateViewModel
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int rolId { get; set; }
        [Required]
        public string rol { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string email { get; set; }
        public string telephone { get; set; }
        public string password { get; set; }       
        public bool act_password { get; set; }
    }
}
