using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Admission
{
    public class CreateViewModel
    {
        [Required]
        public int companyId { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public string invoice { get; set; }
        [Required]
        public string invoicenumber { get; set; }
        [Required]
        public decimal taxes { get; set; }
        [Required]
        public decimal total { get; set; }
       
        [Required]

        public List<DetailViewModel> details { get; set; }
    }
}
