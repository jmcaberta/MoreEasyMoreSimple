

using MoreEasyMoreSimple.Entities.Sales;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoreEasyMoreSimple.Entities.Warehouse
{
     public class Article
    {
        public int articleId { get; set; }
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
        [Required]
        public bool condition { get; set; }

        public Category category { get; set; }       

        public ICollection<Admissiondetails> Admissiondetails { get; set; }
    }
}
