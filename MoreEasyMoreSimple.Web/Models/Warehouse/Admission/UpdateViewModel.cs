using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Warehouse.Admission
{
    public class UpdateViewModel
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
    }
}
