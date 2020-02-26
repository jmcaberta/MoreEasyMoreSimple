using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Sales.Sale
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
        public DateTime saledate { get; set; }
        [Required]
        public decimal taxes { get; set; }
        [Required]
        public decimal total { get; set; }
        public string status { get; set; }

        [Required]

        public List<DetailViewModel> details { get; set; }
    }
}
