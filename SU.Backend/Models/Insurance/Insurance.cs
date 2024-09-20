using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance
{
    public class Insurance
    {
        public int InsuranceId { get; set; } // PK

        // Foreign key to InsurancePolicyHolder (Försäkringstagare)
        public int InsurancePolicyHolderId { get; set; } // FK
        public InsurancePolicyHolder InsurancePolicyHolder { get; set; } // Navigation property

        public int InsuranceCoverageId { get; set; } // FK
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation property for insurance coverage

        // Navigation property for InsuranceAddon
        public ICollection<InsuranceAddon>? InsuranceAddons { get; set; } = new List<InsuranceAddon>(); // Hanterar tillval

        // Insurance-specific fields
        public InsuranceType InsuranceType { get; set; } // Enum for insurance type

        public InsuranceStatus InsuranceStatus { get; set; } // Enum for insurance status
        public PaymentPlan PaymentPlan { get; set; } // Enum for payment plan
        public DateTime StartDate { get; set; } // Försäkringsstartdatum
        public DateTime EndDate { get; set; } // Försäkringsslutdatum (kan vara null om pågående)

        public string Note { get; set; } // Noteringar


    }


}
