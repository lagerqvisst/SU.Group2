using SU.Backend.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance
{
    public class InsurancePolicyHolder
    {
        public int InsurancePolicyHolderId { get; set; } // PK

        // Nullable foreign keys

        /// <summary>
        /// En kund (person eller företag) kan vara försäkringstagare och/eller försäkrad.
        /// </summary>

        public int? CompanyCustomerId { get; set; } // FK, nullable for private customers
        public int? PrivateCustomerId { get; set; } // FK, nullable for company customers

        // Navigational properties

        public Insurance Insurance { get; set; } // Navigation property
        public CompanyCustomer? CompanyCustomer { get; set; } // Nullable object
        public PrivateCustomer? PrivateCustomer { get; set; } // Nullable object
    }

}
