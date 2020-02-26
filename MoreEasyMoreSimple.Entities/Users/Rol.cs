using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Users
{
    public class Rol
    {
        public int rolId { get; set; }
        [Required]
        public string rolname { get; set; }
        [Required]
        public string roldesc { get; set; }
        public bool condition { get; set; } 
        
         public ICollection<Username> Usernames { get; set; }
        
    }
}
