using SU.Backend.Models.Enums.Insurance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    /// <summary>
    /// This class represents the private coverage for an insurance.
    /// It has an instance of PrivateCoverageOption which represents the different options for private coverage.
    /// </summary>
    public class PrivateCoverage
    {
        public int PrivateCoverageId { get; set; } // PK
        public int InsuranceCoverageId { get; set; } // FK to InsuranceCoverage
        public int PrivateCoverageOptionId { get; set; } // FK to PrivateCoverageOption 

        public PrivateCoverageOption PrivateCoverageOption { get; set; } // Navigation to PrivateCoverageOption
        public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation to InsuranceCoverage

        // Personinformation direkt i PrivateCoverage
        public string InsuredPersonName { get; set; }
        public string InsuredPersonPersonalNumber { get; set; }
    }
}
