using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class LiabilityCoverage 
    {
        public int LiabilityCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK till InsuranceCoverage
        public int LiabilityCoverageOptionId { get; set; } // FK till LiabilityCoverageOption
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation till InsuranceCoverage

        public LiabilityCoverageOption LiabilityCoverageOption { get; set; } // Navigation till LiabilityCoverageOption

        public decimal MonthlyPremium { get; set; } // Månadspremie för ansvarsförsäkring
        public decimal CoverageAmount { get; set; } // Belopp för ansvarsförsäkring
    }
}
