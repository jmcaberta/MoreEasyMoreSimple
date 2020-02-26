using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Warehouse
{
    public class Admissiondetails
    {
        public int admissiondetId { get; set; }
        public int admissionId { get; set; }
        public int articleId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }

        public Admission admission { get; set; }
        public Article article { get; set; }
    }
}
