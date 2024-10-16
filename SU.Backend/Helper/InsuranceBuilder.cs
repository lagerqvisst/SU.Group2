using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    public static class InsuranceBuilder
    {
        public static void ApplyAddons(Insurance insurance, List<InsuranceAddonType>? addons)
        {
            if (addons != null && addons.Any())
            {
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
    }
}
