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
            BaseExtraPremium = CalculateExtraPremium(description, coverageAmount);
        }


        public static decimal CalculateExtraPremium(AddonType addonType, decimal coverageAmount)
        {
            decimal extraPremiumRate;

            // Justera påslagsräntan baserat på typ av tillägg
            switch (addonType)
            {
                case AddonType.SicknessAccident:
                    extraPremiumRate = 0.0003m; // Ränta för SicknessAccident
                    break;
                case AddonType.LongTermSickness:
                    extraPremiumRate = 0.0005m; // Ränta för LongTermSickness
                    break;
                default:
                    extraPremiumRate = 0.0003m; // Standardrate om inget annat
                    break;
            }

            // Beräkna den extra premien baserat på belopp och typ
            return coverageAmount * extraPremiumRate;
        }

    }

}
