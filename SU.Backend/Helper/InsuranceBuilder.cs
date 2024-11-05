using SU.Backend.Models.Insurances;

namespace SU.Backend.Helper;

public static class InsuranceBuilder
{
    /// <summary>
    ///     This method helps to apply addons to an insurance.
    ///     It was moved here to improve readability in the insurance create method.
    /// </summary>
    /// <param name="insurance"></param>
    /// <param name="addons"></param>
    public static void ApplyAddons(Insurance insurance, List<InsuranceAddonType>? addons)
    {
        if (addons != null && addons.Any())
            foreach (var addonType in addons)
            {
                var addon = new InsuranceAddon
                {
                    InsuranceAddonType = addonType
                };
                insurance.InsuranceAddons.Add(addon);

                // Addera tilläggets premium till den totala premien
                insurance.Premium += addonType.BaseExtraPremium;
            }
    }
}