using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Invoices
{
    public class InvoiceEntry
    {
        public string Type { get; set; }
        public string CustomerName { get; set; } // För privatkunder
        public string CompanyName { get; set; }  // För företag
        public string PersonalNumber { get; set; }  // För privatkunder
        public string OrganizationNumber { get; set; } // För företag
        public string ContactPerson { get; set; } // För företag
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public decimal Premium { get; set; }
    }

}
