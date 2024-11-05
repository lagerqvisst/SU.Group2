using System.ComponentModel.DataAnnotations;
using SU.Backend.Models.Enums;

namespace SU.Backend.Models.Insurances.Coverage;

public class RiskZone
{
    [Key] public int RiskZoneId { get; set; } // PK
    public RiskzoneLevel RiskZoneLevel { get; set; } // Riskzon
    public double ZoneFactor { get; set; } // Zonfaktor

    // Navigation properties
    public ICollection<VehicleInsuranceCoverage> VehicleInsuranceCoverages { get; set; } =
        new List<VehicleInsuranceCoverage>();
}