using SU.Backend.Models.Customers;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurrance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurrance
{
    public class PrivateInsurance
    {
        public int PrivateInsuranceId { get; set; } // PK

        // Foreign key to InsurancePolicyHolder (Försäkringstagare)
        public int InsurancePolicyHolderNr { get; set; } // FK
        public InsurancePolicyHolder InsurancePolicyHolder { get; set; } // Navigation property

        // Foreign key to InsuredPerson (Försäkrad person)
        public int InsuredPersonId { get; set; } // FK
        public InsuredPerson InsuredPerson { get; set; } // Navigation property

        // Insurance-specific fields
        public PrivateInsuranceType InsuranceType { get; set; } // Enum for insurance type
        public InsuranceStatus InsuranceStatus { get; set; } // Enum for insurance status

        public PaymentPlan PaymentPlan { get; set; } // Enum for payment plan
        public DateTime StartDate { get; set; } // Försäkringsstartdatum
        public DateTime EndDate { get; set; } // Försäkringsslutdatum (kan vara null om pågående)

        // Navigation property for PrivateCustomer
        public int? PrivateCustomerId { get; set; } // Nullable FK
        public PrivateCustomer? PrivateCustomer { get; set; } // Navigation property
    }

}
