namespace SU.Backend.Models.Insurances.Coverage;

/// <summary>
///     This class represents the liability coverage for an insurance.
///     It has an instance of LiabilityCoverageOption which represents the different options for liability coverage.
/// </summary>
public class LiabilityCoverage
{
    public int LiabilityCoverageId { get; set; } // PK
    public int InsuranceCoverageId { get; set; } // FK to InsuranceCoverage
    public int LiabilityCoverageOptionId { get; set; } // FK to LiabilityCoverageOption
    public InsuranceCoverage InsuranceCoverage { get; set; } // Navigation to InsuranceCoverage

    public LiabilityCoverageOption LiabilityCoverageOption { get; set; } // Navigation to LiabilityCoverageOption
}