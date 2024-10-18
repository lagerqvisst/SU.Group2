using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurances.Prospects;

namespace SU.Backend.Models.Insurances
{
    /// <summary>
    /// This class represents the insurance policy holder for an insurance.
    /// A private or company customer can exist in the system without being a policy holder.
    /// A policy holder can only be connected to one insurance.
    /// But a private or company customer can be connected to multiple insurances and therefor be multiple policy holders.
    /// </summary>
    public class InsurancePolicyHolder
    {
        public int InsurancePolicyHolderId { get; set; } // PK

        // Nullable because a policy holder can be a company or private customer.
        public int? CompanyCustomerId { get; set; } // FK, nullable
        public int? PrivateCustomerId { get; set; } // FK, nullable

        public int InsuranceId { get; set; }

        // Navigation properties
        public Insurance Insurance { get; set; }

        public CompanyCustomer? CompanyCustomer { get; set; } // Nullable object
        public PrivateCustomer? PrivateCustomer { get; set; } // Nullable object
    }




}
