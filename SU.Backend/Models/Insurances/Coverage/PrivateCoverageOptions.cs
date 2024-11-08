using SU.Backend.Models.Enums.Insurance;

namespace SU.Backend.Models.Insurances.Coverage;

/// <summary>
///     This class represents the different private coverage options for an insurance.
///     Reflects the available options for private coverage in the business documentation.
/// </summary>
public class PrivateCoverageOption
{
    public int PrivateCoverageOptionId { get; set; } // PK
    public decimal CoverageAmount { get; set; } // Grundbelopp
    public decimal MonthlyPremium { get; set; }
    public DateTime StartDate { get; set; }
    public InsuranceType InsuranceType { get; set; }

    // Navigational property to many PrivateCoverages
    public ICollection<PrivateCoverage> PrivateCoverages { get; set; } = new List<PrivateCoverage>();
}