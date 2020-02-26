using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Sales
{
    public class Status
    {
        public int statusId { get; set; }
        [Required]
        public string statusname { get; set; }
        public bool condition { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
