using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances.Prospects;

namespace SU.Backend.Models.Insurances
{
    public class InsurancePolicyHolder
    {
        public int InsurancePolicyHolderId { get; set; } // PK
        public int? CompanyCustomerId { get; set; } // FK, nullable
        public int? PrivateCustomerId { get; set; } // FK, nullable

        // Här är ICollection för att representera flera försäkringar
        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>(); // Navigation property

        public CompanyCustomer? CompanyCustomer { get; set; } // Nullable object
        public PrivateCustomer? PrivateCustomer { get; set; } // Nullable object
    }




}
