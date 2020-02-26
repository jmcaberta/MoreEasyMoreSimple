using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoreEasyMoreSimple.Web.Models.Sales.Contact
{
    public class UpdateViewModel
    {
        public int contactId { get; set; }
        public int companyId { get; set; }        
        public string title { get; set; }
        public string contactname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string comment { get; set; }
    }
}
