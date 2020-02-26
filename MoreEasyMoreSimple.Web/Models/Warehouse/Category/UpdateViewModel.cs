using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Category
{
    public class UpdateViewModel
    {
        public int categoryId { get; set; }
        [Required]
        public string categoryname { get; set; }
        [StringLength(256)]
        public string categorydesc { get; set; }       
       
    }
}
