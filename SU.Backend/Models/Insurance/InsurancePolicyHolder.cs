﻿using SU.Backend.Models.Customers;
using SU.Backend.Models.Insurance.Prospects;

namespace SU.Backend.Models.Insurance
{
    public class InsurancePolicyHolder
    {
        public int InsurancePolicyHolderId { get; set; } // PK

        public int? CompanyCustomerId { get; set; } // FK, nullable
        public int? PrivateCustomerId { get; set; } // FK, nullable

        public Insurance Insurance { get; set; } // Navigation property

        public CompanyCustomer? CompanyCustomer { get; set; } // Nullable object
        public PrivateCustomer? PrivateCustomer { get; set; } // Nullable object
    }



}
