using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Category
{
    public class CreateViewModel
    {
        
        [Required]
        public string categoryname { get; set; }
        [StringLength(256)]
        public string categorydesc { get; set; }       
       
    }
}
