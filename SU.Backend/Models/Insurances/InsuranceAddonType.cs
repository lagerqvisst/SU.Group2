using SU.Backend.Helper;
using SU.Backend.Models.Enums.Insurance.Addons;

namespace SU.Backend.Models.Insurances;

/// <summary>
///     This class represents the different types of addons that can be added to an insurance.
///     Options reflect the available types of addons in the business documentation.
/// </summary>
public class InsuranceAddonType
{
    public InsuranceAddonType()
    {
        // Tom konstruktor
    }

    public InsuranceAddonType(AddonType description, decimal coverageAmount)
    {
        Description = description;
        CoverageAmount = coverageAmount;
        BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(description, coverageAmount);
    }

    public int InsuranceAddonTypeId { get; set; } // PK

    public AddonType Description { get; set; } // Description of the addon
    public decimal CoverageAmount { get; set; } // Grundbelopp
    public decimal BaseExtraPremium { get; set; } // Grundkostnad för tillägget
}