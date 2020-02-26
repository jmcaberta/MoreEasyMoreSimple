using MoreEasyMoreSimple.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Sales
{
    public class Sale
    {
        public int saleId { get; set; }
        public int companyId { get; set; }
        public int userId { get; set; }
        public string invoice { get; set; }
        public string invoicenumber { get; set; }
        public DateTime saledate { get; set; }
        public decimal taxes { get; set; }
        public decimal total { get; set; }
        public string status { get; set; }

        public ICollection<Saledetails> details { get; set; }
        public Company company { get; set; }
        public Username user { get; set; }
    }
}
