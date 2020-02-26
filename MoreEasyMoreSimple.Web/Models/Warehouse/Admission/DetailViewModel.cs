using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Admission
{
    public class DetailViewModel
    {
        [Required]
        public int articleId { get; set; }
        public string article { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public decimal price { get; set; }
    }
}
