using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Users.Username
{
    public class UsernameViewModel
    {
        public int userId { get; set; }
        public int rolId { get; set; }
        public string rol { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public byte[] password_hash { get; set; }        
        public bool condition { get; set; }
    }
}
