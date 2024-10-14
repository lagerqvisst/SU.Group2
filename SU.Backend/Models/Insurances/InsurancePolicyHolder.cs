using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances.Prospects;

namespace SU.Backend.Models.Insurances
{
    public class InsurancePolicyHolder
    {
        public int InsurancePolicyHolderId { get; set; } // PK
        public int? CompanyCustomerId { get; set; } // FK, nullable
        public int? PrivateCustomerId { get; set; } // FK, nullable

        public int InsuranceId { get; set; }

        // En försäkringstagar kan enbart vara kopplad till en försäkring.
        public Insurance Insurance { get; set; }

        public CompanyCustomer? CompanyCustomer { get; set; } // Nullable object
        public PrivateCustomer? PrivateCustomer { get; set; } // Nullable object
    }




}
