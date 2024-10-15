using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances
{
    public class Insurance
    {
        public int InsuranceId { get; set; } // PK

        // Foreign key to InsurancePolicyHolder (Försäkringstagare)
        public InsurancePolicyHolder InsurancePolicyHolder { get; set; } // Navigation property
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation property for insurance coverage

        // Navigation property for InsuranceAddon
        public ICollection<InsuranceAddon>? InsuranceAddons { get; set; } = new List<InsuranceAddon>(); // Hanterar tillval

        [ForeignKey("Seller")]
        public int SellerId { get; set; } // FK to Employee
        public Employee Seller { get; set; } // Navigation property for seller

        // Insurance-specific fields

        public decimal Premium { get; set; } // Premie
        public InsuranceType InsuranceType { get; set; } // Enum for insurance type
        public InsuranceStatus InsuranceStatus { get; set; } // Enum for insurance status
        public PaymentPlan PaymentPlan { get; set; } // Enum for payment plan
        public DateTime StartDate { get; set; } // Försäkringsstartdatum
        public DateTime EndDate { get; set; } // Försäkringsslutdatum (kan vara null om pågående)

        public string Note { get; set; } // Noteringar


    }


}
