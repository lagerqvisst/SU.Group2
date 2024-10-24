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
        public int insuranceId { get; set; } // PK

        // Foreign key to InsurancePolicyHolder (Försäkringstagare)
        public InsurancePolicyHolder insurancePolicyHolder { get; set; } // Navigation property
        public InsuranceCoverage insuranceCoverage { get; set; } // Navigation property for insurance coverage

        // Navigation property for InsuranceAddon
        public ICollection<InsuranceAddon>? insuranceAddons { get; set; } = new List<InsuranceAddon>(); // Hanterar tillval

        [ForeignKey("Seller")]
        public int sellerId { get; set; } // FK to Employee
        public Employee seller { get; set; } // Navigation property for seller

        // Insurance-specific fields

        public InsuranceCategory insuranceCategory { get; set; } // Private/Company
        public decimal premium { get; set; } // Premie
        public InsuranceType insuranceType { get; set; } // Enum for insurance type
        public InsuranceStatus insuranceStatus { get; set; } // Enum for insurance status
        public PaymentPlan paymentPlan { get; set; } // Enum for payment plan
        public DateTime startDate { get; set; } // Försäkringsstartdatum
        public DateTime endDate { get; set; } // Försäkringsslutdatum (kan vara null om pågående)

        public string note { get; set; } // Noteringar


    }


}
