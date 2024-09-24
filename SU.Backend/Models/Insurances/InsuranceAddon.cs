using SU.Backend.Models.Enums.Insurance.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances
{
    public class InsuranceAddon
    {
        public int InsuranceAddonId { get; set; } // PK
        
        public int InsuranceAddonTypeId { get; set; } // FK
        public InsuranceAddonType InsuranceAddonType { get; set; } // Navigation property

        // Foreign key to PrivateInsurance
        public int InsuranceId { get; set; } // FK
        public Insurance Insurance { get; set; } // Navigation property

        
    }


}
