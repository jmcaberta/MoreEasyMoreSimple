using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Sales.Sale
{
    public class SalesViewModel
    {
        public int saleId { get; set; }
        public int companyId { get; set; }
        public string company { get; set; }
        public string ustId { get; set; }
        public string adress { get; set; }
        public string city { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int userId { get; set; }
        public string user { get; set; }
        public string invoice { get; set; }
        public string invoicenumber { get; set; }
        public DateTime saledate { get; set; }
        public decimal taxes { get; set; }
        public decimal total { get; set; }
        public string status { get; set; }
    }
}
