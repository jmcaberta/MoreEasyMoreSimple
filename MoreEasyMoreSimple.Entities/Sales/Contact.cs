using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Sales
{
    public class Contact
    {
        public int contactId { get; set; }
        [Required]
        public int companyId { get; set; }
        public string title { get; set; }
        [Required]
        public string contactname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phone { get; set; }
        public string comment { get; set; }
        public bool condition { get; set; }

        public Company company { get; set; }

    }
}
