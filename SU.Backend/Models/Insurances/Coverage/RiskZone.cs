using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurances.Coverage
{
    public class Rizkzone
    {

        [Key] public int RiskzoneId { get; set; } // PK
        public RiskzoneLevel RiskzoneLevel { get; set; } // Riskzon
        public double ZoneFactor { get; set; } // Zonfaktor

        // Navigation properties
        public ICollection<VehicleInsuranceCoverage> VehicleInsuranceCoverages { get; set; } = new List<VehicleInsuranceCoverage>();
    }
}