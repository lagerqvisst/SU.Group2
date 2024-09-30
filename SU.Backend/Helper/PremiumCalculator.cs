using SU.Backend.Models.Insurances.Coverage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Helper
{
    public class PremiumCalculator
    {
        public static decimal GetVehicleInsurancePremium(Riskzone riskzone, VehicleInsuranceOption vehicleInsuranceOption)
        {
            if (riskzone == null || vehicleInsuranceOption == null)
            {
                throw new ArgumentNullException("Riskzone or VehicleInsuranceOption cannot be null.");
            }

            // Get the base cost from the selected vehicle insurance option
            decimal baseCost = vehicleInsuranceOption.OptionCost;

            // Get the zone factor from the riskzone object
            double zoneFactor = riskzone.ZoneFactor;

            // Calculate the premium
            decimal premium = baseCost * (decimal)zoneFactor;

            return premium;
        }
    }

}
