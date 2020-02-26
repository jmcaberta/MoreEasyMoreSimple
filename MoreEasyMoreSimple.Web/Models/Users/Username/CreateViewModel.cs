using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Users.Username
{
    public class CreateViewModel
    {
        public int userId { get; set; }
        [Required]
        public int rolId { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string email { get; set; }
        public string telephone { get; set; }
        [Required]
        public string password { get; set; }     
       
    }
}
