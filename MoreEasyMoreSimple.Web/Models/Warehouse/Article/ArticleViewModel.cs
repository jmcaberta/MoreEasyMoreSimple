using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Article
{
    public class ArticleViewModel
    {
        public int articleId { get; set; }        
        public int categoryId { get; set; }
        public string category { get; set; }       
        public string codearticle { get; set; }        
        public string articlename { get; set; }       
        public decimal sellprice { get; set; }        
        public int stock { get; set; }
        public string articledesc { get; set; }        
        public bool condition { get; set; }
    }
}
