using MoreEasyMoreSimple.Entities.Sales;
using MoreEasyMoreSimple.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Warehouse
{
    public class Admission
    {
        public int admissionId { get; set; }
        public int companyId { get; set; }
        public int userId { get; set; }
        public string invoice { get; set; }
        public string invoicenumber { get; set; }
        public DateTime admissiondate { get; set; }
        public decimal taxes { get; set; }
        public decimal total { get; set; }
        public string status { get; set; }
        
        public ICollection<Admissiondetails> details { get; set; }
        public Company company { get; set; }
        public Username user { get; set; }
    }
}
