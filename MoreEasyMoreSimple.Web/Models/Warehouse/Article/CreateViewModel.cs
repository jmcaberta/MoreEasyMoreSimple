using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Article
{
    public class CreateViewModel
    {        
        [Required]
        public int categoryId { get; set; }       
        [Required]
        public string codearticle { get; set; }
        [Required]
        public string articlename { get; set; }
        [Required]
        public decimal sellprice { get; set; }
        [Required]
        public int stock { get; set; }
        public string articledesc { get; set; }
        
    }
}
