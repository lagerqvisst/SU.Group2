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
        public double ZoneFactor { get; set; } // Zonfaktor

        // Navigation property
        public VehicleInsuranceCoverage VehicleInsuranceCoverage { get; set; } // Navigation till VehicleInsuranceCoverage
    }
}