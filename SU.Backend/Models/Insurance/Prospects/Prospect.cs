using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance.Prospects
{
    public class Prospect
    {
        public int ProspectId { get; set; } // Primary Key

        // Status of the prospect
        public ProspectStatus ProspectStatus { get; set; } // Status of the prospect

        // Foreign keys
        public int? PrivateCustomerId { get; set; }
        public int? CompanyCustomerId { get; set; }

        // Navigation properties
        public PrivateCustomer? PrivateCustomer { get; set; }
        public CompanyCustomer? CompanyCustomer { get; set; }
    }

}
