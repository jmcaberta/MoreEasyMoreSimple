using MoreEasyMoreSimple.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreEasyMoreSimple.Entities.Sales
{
    public class Company
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
        public bool condition { get; set; }

        public Branch branch { get; set; }
        public Status status { get; set; }

        public ICollection<Contact> Contacts { get; set; }        
        public ICollection<Admission> Admissions { get; set; }       
       
    }
}
