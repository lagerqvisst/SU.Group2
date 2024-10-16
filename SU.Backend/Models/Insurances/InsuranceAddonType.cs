using SU.Backend.Helper;
using SU.Backend.Models.Enums.Insurance.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances
{
    public class InsuranceAddonType
    {
        public int InsuranceAddonTypeId { get; set; } // PK
        // Specifika egenskaper för tilläggstypen
        public AddonType Description { get; set; } // En beskrivning av tillägget
        public decimal CoverageAmount { get; set; } // Grundbelopp
        public decimal BaseExtraPremium { get; set; } // Grundkostnad för tillägget

        public InsuranceAddonType()
        {
            // Tom konstruktor
        }

        public InsuranceAddonType(AddonType description, decimal coverageAmount)
        {
            Description = description;
            CoverageAmount = coverageAmount;
            BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(description, coverageAmount);
        }


    }

}
