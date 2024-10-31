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
        public ProspectType ProspectType { get; set; } // Type of prospect
        public ProspectStatus ProspectStatus { get; set; } // Status of the prospect

        // Additional properties for print
        public string? FirstName => PrivateCustomer?.FirstName ?? CompanyCustomer?.CompanyName;
        public string? LastName => PrivateCustomer?.LastName;
        public string? PersonalOrOrgNumber => PrivateCustomer?.PersonalNumber ?? CompanyCustomer?.OrganizationNumber;
        public string? StreetAddress => PrivateCustomer?.Address ?? CompanyCustomer?.CompanyAdress;
        public string? PhoneNumber => PrivateCustomer?.PhoneNumber ?? CompanyCustomer?.CompanyPhoneNumber;
        public string? Email => PrivateCustomer?.Email ?? CompanyCustomer?.CompanyEmailAdress;

        // Status of the prospect

        public string? ContactNote { get; set; } // Note of contact

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
