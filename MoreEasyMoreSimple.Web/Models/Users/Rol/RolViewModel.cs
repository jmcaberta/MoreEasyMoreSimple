using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Users.Rol
{
    public class RolViewModel
    {
        public int rolId { get; set; }      
        public string rolname { get; set; }       
        public string roldesc { get; set; }
        public bool condition { get; set; }
    }
}
