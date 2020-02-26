using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Category
{
    public class CategoryViewModel
    {
        public int categoryId { get; set; }       
        public string categoryname { get; set; }       
        public string categorydesc { get; set; }       
        public bool condition { get; set; }
    }
}
