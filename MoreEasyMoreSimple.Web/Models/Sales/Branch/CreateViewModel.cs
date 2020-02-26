using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Sales.Branch
{
    public class CreateViewModel
    {       
        [Required]
        public string branchname { get; set; }
        [Required]
        public string branchdesc { get; set; }       
    }
}
