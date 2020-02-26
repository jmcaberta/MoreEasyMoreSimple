using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoreEasyMoreSimple.Entities.Warehouse
{
    public class Category
    {
        public int categoryId { get; set; }
        [Required]
        public string categoryname { get; set; }
        [StringLength (256)]
        public string categorydesc { get; set; }
        public bool condition { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
