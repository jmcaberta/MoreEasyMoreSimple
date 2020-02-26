using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Users
{
    public class Username
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
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        public bool condition { get; set; }

        public Rol rol { get; set; }

        public ICollection<Admission> Admissions { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
