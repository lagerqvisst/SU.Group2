using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Models.Enums.Prospects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SU.Backend.Models.Insurances.Prospects
{
    public class Prospect
    {
        public int ProspectId { get; set; } // Primary Key

        // Status of the prospect
        public ProspectStatus ProspectStatus { get; set; } // Status of the prospect

        public ProspectType ProspectType { get; set; } // Type of prospect
        public DateTime? ContactDate { get; set; } // Date of contact
        public string? AssignedAgentNumber { get; set; } // Assigned agent number

        // Foreign keys
        public int? PrivateCustomerId { get; set; }
        public int? CompanyCustomerId { get; set; }

        [ForeignKey("Seller")]
        public int? SellerId { get; set; }

        // Navigation properties
        public PrivateCustomer? PrivateCustomer { get; set; }
        public CompanyCustomer? CompanyCustomer { get; set; }
        public Employee? Seller { get; set; }
    }

}
