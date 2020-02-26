using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Sales
{
    public class Branch
    {
        public int branchId { get; set; }
        [Required]
        public string branchname { get; set; }
        [Required]
        public string branchdesc { get; set; }
        public bool condition { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
