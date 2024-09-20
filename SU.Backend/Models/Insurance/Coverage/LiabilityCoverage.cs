using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance.Coverage
{
    public class LiabilityCoverage 
    {
        public int LiabilityCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK till InsuranceCoverage

        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation till InsuranceCoverage

        public decimal MonthlyPremium { get; set; } // Månadspremie för ansvarsförsäkring
        public decimal CoverageAmount { get; set; } // Belopp för ansvarsförsäkring
    }
}
