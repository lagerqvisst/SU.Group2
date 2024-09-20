using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance.Coverage
{
    public class PrivateCoverageOption
    {
        public int PrivateCoverageOptionId { get; set; } // PK
        public decimal CoverageAmount { get; set; } // Grundbelopp
        public decimal MonthlyPremium { get; set; } // Månadspremie
        public DateTime StartDate { get; set; } // Datum när alternativet börjar gälla
        public InsuranceType InsuranceType { get; set; } 
    }
}
