using SU.Backend.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Insurance.Coverage
{
    public class RizkZone
    {
        public int RiskZoneId { get; set; } // PK
        public RiskZoneLevel RiskZoneLevel { get; set; } // Riskzon
        public double ZoneFactor { get; set; } // Zonfaktor

        // Navigation properties
        public ICollection<VehicleInsuranceCoverage> VehicleInsuranceCoverages { get; set; } = new List<VehicleInsuranceCoverage>();
    }
}