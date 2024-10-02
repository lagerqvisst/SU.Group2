using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class LiabilityCoverageOption
    {
        [Key] public int LiabilityCoverageOptionId { get; set; }
        public Deductible Deductible { get; set; } // Självrisk
        public LiabilityCoverageOptionAmounts OptionAmount { get; set; } // 3, 5 eller 10 miljoner
        public decimal MonthlyPremium { get; set; } // Månadspremie för vald belopp
        public ICollection<LiabilityCoverage> LiabilityCoverages { get; set; } = new List<LiabilityCoverage>();

    }
}
