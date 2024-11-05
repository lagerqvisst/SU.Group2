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

        // Justera påslagsräntan baserat på typ av tillägg
        switch (addonType)
        {
            case AddonType.SicknessAccident:
                extraPremiumRate = 0.0003m; // Ränta för SicknessAccident
                break;
            case AddonType.LongTermSickness:
                extraPremiumRate = 0.0005m; // Ränta för LongTermSickness
                break;
            default:
                extraPremiumRate = 0.0003m; // Standardrate om inget annat
                break;
        }

        // Beräkna den extra premien baserat på belopp och typ
        return coverageAmount * extraPremiumRate;
    }

    #endregion

    #region Property & Inventory Insurance

    public static decimal CalculatePropertyPremium(decimal propertyValue)
    {
        const decimal premiumRate = 0.002m; // 0.2% rate
        return propertyValue * premiumRate;
    }

    // Statisk metod för att beräkna inventariepremien
    public static decimal CalculateInventoryPremium(decimal inventoryValue)
    {
        const decimal premiumRate = 0.002m; // 0.2% rate
        return inventoryValue * premiumRate;
    }

    // Statisk metod för att summera den totala premien
    public static decimal CalculateTotalPropertyAndInventoryPremium(decimal propertyPremium, decimal inventoryPremium)
    {
        return propertyPremium + inventoryPremium;
    }

    #endregion
}