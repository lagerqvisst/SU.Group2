using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Models.Insurances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU.Backend.Services.Interfaces;
using Microsoft.Extensions.Logging;
using SU.Backend.Services;
using SU.Backend.Models.Enums.Insurance;

namespace SU.Backend.Controllers
{
    /// <summary>
    /// This class is responsible for handling the insurance listing controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class InsuranceListingController
    {
        // Services
        private readonly IInsuranceListingService _insuranceListingService;   
        private readonly ILogger<InsuranceListingController> _logger;

        // Constructor
        public InsuranceListingController(IInsuranceListingService insuranceListingService, ILogger<InsuranceListingController> logger)
        {
            _insuranceListingService = insuranceListingService;
            _logger = logger;
        }

        // Controller for GetAllVehicleInsuranceCoverages method
        public async Task<(List<VehicleInsuranceCoverage> vehicleInsuranceCoverages, string message)> GetAllVehicleInsuranceCoverages()
        {
            _logger.LogInformation("Controller activated to list all vehicle insurance coverages...");
            var result = await _insuranceListingService.GetAllVehicleInsuranceCoverages();

            if (result.success)
            {
                _logger.LogInformation($"Vehicle insurance coverages retrieved succesfully:\n{result.message}");
                return (result.vehicleInsuranceCoverages, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving vehicle insurance coverages: {result.message}");
                return (new List<VehicleInsuranceCoverage>(), result.message);
            }
        }

        // Controller for GetAllVehicleInsuranceOptions method
        public async Task<(List<VehicleInsuranceOption> vehicleInsuranceOptions, string Message)> GetAllVehicleInsuranceOptions()
        {
            _logger.LogInformation("Controller activated to list all vehicle insurance options...");

            var result = await _insuranceListingService.GetAllVehicleInsuranceOptions();

            if (result.success) {
                _logger.LogInformation($"Vehicle insurance options retrieved successfully:\n{result.message}");
                return (result.vehicleInsuranceOptions, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving vehicle insurance options: {result.message}");
                return (new List<VehicleInsuranceOption>(), result.message);

            }
        }

        // Controller for GetAllInsurances method
        public async Task<(List<Insurance> insurances, string message)> GetAllInsurances()
        {
            _logger.LogInformation("Controller activated to list all insurances...");
            var result = await _insuranceListingService.GetAllInsurances();

            if (result.success)
            {
                _logger.LogInformation($"Insurances retrieved successfully:\n{result.message}");
                return (result.insurances, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurances: {result.message}");
                return (new List<Insurance>(), result.message);
            }
        }

        // Controller for GetAllInsuranceAddonTypes method
        public async Task<(bool Success, List<InsuranceAddonType> insuranceAddonTypes, string Message)> GetAllInsuranceAddonTypes()
          {
              _logger.LogInformation("Controller activated to list all insurance addon types...");
              var result = await _insuranceListingService.GetAllInsuranceAddonTypes();

              if (result.success)
              {
                  _logger.LogInformation($"Insurance addon types retrieved successfully:\n{result.message}");
                  return (true, result.insuranceAddonTypes, result.message);
              }
              else
              {
                  _logger.LogWarning($"Error retrieving insurance addon types: {result.message}");
                  return (false, new List<InsuranceAddonType>(), result.message);
              }
          }

        // Controller for GetAllInsuranceAddons method
        public async Task<(List<InsuranceAddon> insuranceAddons, string message)> GetAllInsuranceAddons()
        {
            _logger.LogInformation("Controller activated to list all insurance addons...");
            var result = await _insuranceListingService.GetAllInsuranceAddons();

            if (result.success)
            {
                _logger.LogInformation($"Insurance addons retrieved succesfully:\n{result.message}");
                return (result.insuranceAddons, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance addons: {result.message}");
                return (new List<InsuranceAddon>(), result.message);
            }
        }

        // Controller for GetAllInsurancePolicyHolders method
        public async Task<(List<InsurancePolicyHolder> insurancePolicyHolders, string message)> GetAllInsurancePolicyHolders()
        {
            _logger.LogInformation("Controller activated to list all insurance policy holders...");
            var result = await _insuranceListingService.GetAllInsurancePolicyHolders();

            if (result.success)
            {
                _logger.LogInformation($"Insurance policy holders retrieved succesfully:\n{result.message}");
                return (result.insurancePolicyHolders, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance policy holders: {result.message}");
                return (new List<InsurancePolicyHolder>(), result.message);
            }
        }

        // Controller for GetAllInsuranceCoverages method
        public async Task<(List<InsuranceCoverage> insuranceCoverages, string message)> GetAllInsuranceCoverages()
        {
            _logger.LogInformation("Controller activated to list all insurance coverages...");
            var result = await _insuranceListingService.GetAllInsuranceCoverages();

            if (result.success)
            {
                _logger.LogInformation($"Insurance coverages retrieved succesfully:\n{result.message}");
                return (result.insuranceCoverages, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving insurance coverages: {result.message}");
                return (new List<InsuranceCoverage>(), result.message);
            }
        }

        // Controller for GetAllLiabilityCoverages method
        public async Task<(List<LiabilityCoverage> liabilityCoverages, string message)> GetAllLiabilityCoverages()
        {
            _logger.LogInformation("Controller activated to list all liability coverages...");
            var result = await _insuranceListingService.GetAllLiabilityCoverages();

            if (result.success)
            {
                _logger.LogInformation($"Liability coverages retrieved succesfully:\n{result.message}");
                return (result.liabilityCoverages, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving liability coverages: {result.message}");
                return (new List<LiabilityCoverage>(), result.message);
            }
        }

        // Controller for GetAllLiabilityCoverageOptions method
        public async Task<(List<LiabilityCoverageOption> liabilityCoverageOptions, string message)> GetAllLiabilityCoverageOptions()
        {
            _logger.LogInformation("Controller activated to list all liability coverages...");
            var result = await _insuranceListingService.GetAllLiabilityCoverageOptions();

            if (result.success)
            {
                _logger.LogInformation($"Liability coverages retrieved succesfully:\n{result.message}");
                return (result.liabilityCoverageOptions, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving liability coverages: {result.message}");
                return (new List<LiabilityCoverageOption>(), result.message);
            }
        }

        // Controller for GetAllRiskZones method
        public async Task<(List<RiskZone> riskzones, string message)> GetAllRiskZones()
        {
            _logger.LogInformation("Controller activated to list all riskzones...");
            var result = await _insuranceListingService.GetAllRiskZones();

            if (result.success)
            {
                _logger.LogInformation($"Riskzones retrieved succesfully:\n{result.message}");
                return (result.riskZones, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving riskzones: {result.message}");
                return (new List<RiskZone>(), result.message);
            }
        }

        // Controller for GetAllPropertyAndInventoryCoverages method
        public async Task<(List<PropertyAndInventoryCoverage> propertyAndInventoryCoverages, string message)> GetAllPropertyAndInventoryCoverages()
        {
            _logger.LogInformation("Controller activated to list all property and inventory coverages...");
            var result = await _insuranceListingService.GetAllPropertyAndInventoryCoverages();

            if (result.success)
            {
                _logger.LogInformation($"Property and inventory coverages retrieved succesfully:\n{result.message}");
                return (result.propertyAndInventoryCoverages, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving property and inventory coverages: {result.message}");
                return (new List<PropertyAndInventoryCoverage>(), result.message);
            }
        }

        // Controller for GetSpecificPrivateOption method
        public async Task<(List<PrivateCoverageOption> PrivateCoverageOptions, string Message)> GetSpecificPrivateOption(InsuranceType insuranceType)
        {
            _logger.LogInformation("Controller activated to get specific private option...");
            var result = await _insuranceListingService.GetSpecificPrivateOption(insuranceType);

            if (result.Success)
            {
                _logger.LogInformation($"Private option found:\n{result.Message}");
                return (result.PrivateCoverageOptions, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error getting private option: {result.Message}");
                return (new List<PrivateCoverageOption>(), result.Message);
            }
        }
    }
}
