using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances.Coverage;

namespace SU.Backend.Helper;

/// <summary>
///     This class is responsible for calculating the insurance premiums. Reflects the business documentation.
///     It collects all the different calculations provided in the business documentation.
///     It was moved here to improve readability in the insurance create method.
/// </summary>
public class PremiumCalculator
{
    #region VehicleInsurance

    public static decimal GetVehicleInsurancePremium(RiskZone riskZone, VehicleInsuranceOption vehicleInsuranceOption)
    {
        if (riskZone == null || vehicleInsuranceOption == null)
            throw new ArgumentNullException("Riskzone or VehicleInsuranceOption cannot be null.");

        // Get the base cost from the selected vehicle insurance option
        var baseCost = vehicleInsuranceOption.OptionCost;

        // Get the zone factor from the riskzone object
        var zoneFactor = riskZone.ZoneFactor;

        // Calculate the premium
        var premium = baseCost * (decimal)zoneFactor;

        return premium;
    }

    #endregion

    #region Addons

    public static decimal CalculateAddonExtraPremium(AddonType addonType, decimal coverageAmount)
    {
        decimal extraPremiumRate;

        // Adjust the extra premium rate based on the type of addon
        switch (addonType)
        {
            case AddonType.SicknessAccident:
                extraPremiumRate = 0.0003m; // Rate for SicknessAccident
                break;
            case AddonType.LongTermSickness:
                extraPremiumRate = 0.0005m; // Rate for LongTermSickness
                break;
            default:
                extraPremiumRate = 0.0003m; // Standard rate for other addons
                break;
        }

        // Calculate the extra premium based on the coverage amount and the extra premium rate
        return coverageAmount * extraPremiumRate;
    }

    #endregion

    #region Property & Inventory Insurance

    public static decimal CalculatePropertyPremium(decimal propertyValue)
    {
        const decimal premiumRate = 0.002m; // 0.2% rate
        return propertyValue * premiumRate;
    }

    // Static method to calculate the inventory premium
    public static decimal CalculateInventoryPremium(decimal inventoryValue)
    {
        const decimal premiumRate = 0.002m; // 0.2% rate
        return inventoryValue * premiumRate;
    }

    // Static method to calculate the total premium
    public static decimal CalculateTotalPropertyAndInventoryPremium(decimal propertyPremium, decimal inventoryPremium)
    {
        return propertyPremium + inventoryPremium;
    }

    #endregion
}