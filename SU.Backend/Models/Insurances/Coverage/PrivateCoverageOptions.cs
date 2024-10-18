using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    /// <summary>
    /// This class represents the different private coverage options for an insurance.
    /// Reflects the available options for private coverage in the business documentation.
    /// </summary>
    public class PrivateCoverageOption
    {
        public int PrivateCoverageOptionId { get; set; } // PK
        public decimal CoverageAmount { get; set; } // Grundbelopp
        public decimal MonthlyPremium { get; set; } 
        public DateTime StartDate { get; set; } 
        public InsuranceType InsuranceType { get; set; }

        // Navigational property till många PrivateCoverages
        public ICollection<PrivateCoverage> PrivateCoverages { get; set; } = new List<PrivateCoverage>();

    }
}
