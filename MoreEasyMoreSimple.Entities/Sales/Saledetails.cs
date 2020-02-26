using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Sales
{
    public class Saledetails
    {
        public int saledetailId { get; set; }
        public int saleId { get; set; }
        public int articleId { get; set; }
        public int quantity { get; set; }
        public decimal discount { get; set; }
        public decimal price { get; set; } 
        public decimal percent { get; set; }

        public Sale sale { get; set; }
        public Article article { get; set; }
    }
}
