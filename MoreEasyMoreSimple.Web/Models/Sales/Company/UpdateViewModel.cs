using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Sales.Company
{
    public class UpdateViewModel
    {
        public int companyId { get; set; }
        public int statusId { get; set; }
        public int branchId { get; set; }
        public string ustId { get; set; }
        public string companyname { get; set; }
        public string adress { get; set; }
        public string city { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}
