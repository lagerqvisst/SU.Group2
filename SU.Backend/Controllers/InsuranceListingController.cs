using Microsoft.Extensions.Logging;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Controllers;

/// <summary>
///     This class is responsible for handling the insurance listing controller.
///     Makes logic available in the Viewmodel
///     More info about the logic for each method can be found in the Service function each controller method uses.
/// </summary>
public class InsuranceListingController
{
    // Services
    private readonly IInsuranceListingService _insuranceListingService;
    private readonly ILogger<InsuranceListingController> _logger;

    // Constructor
    public InsuranceListingController(IInsuranceListingService insuranceListingService,
        ILogger<InsuranceListingController> logger)
    {
        _insuranceListingService = insuranceListingService;
        _logger = logger;
    }

    // Controller for GetAllVehicleInsuranceCoverages method
    public async Task<(List<VehicleInsuranceCoverage> vehicleInsuranceCoverages, string message)>
        GetAllVehicleInsuranceCoverages()
    {
        _logger.LogInformation("Controller activated to list all vehicle insurance coverages...");
        var result = await _insuranceListingService.GetAllVehicleInsuranceCoverages();

        if (result.success)
        {
            _logger.LogInformation($"Vehicle insurance coverages retrieved successfully:\n{result.message}");
            return (result.vehicleInsuranceCoverages, result.message);
        }

        _logger.LogWarning($"Error retrieving vehicle insurance coverages: {result.message}");
        return (new List<VehicleInsuranceCoverage>(), result.message);
    }

    // Controller for GetAllVehicleInsuranceOptions method
    public async Task<(List<VehicleInsuranceOption> vehicleInsuranceOptions, string Message)>
        GetAllVehicleInsuranceOptions()
    {
        _logger.LogInformation("Controller activated to list all vehicle insurance options...");

        var result = await _insuranceListingService.GetAllVehicleInsuranceOptions();

        if (result.success)
        {
            _logger.LogInformation($"Vehicle insurance options retrieved successfully:\n{result.message}");
            return (result.vehicleInsuranceOptions, result.message);
        }

        _logger.LogWarning($"Error retrieving vehicle insurance options: {result.message}");
        return (new List<VehicleInsuranceOption>(), result.message);
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

        _logger.LogWarning($"Error retrieving insurances: {result.message}");
        return (new List<Insurance>(), result.message);
    }

    // Controller for GetAllInsuranceAddonTypes method
    public async Task<(bool Success, List<InsuranceAddonType> insuranceAddonTypes, string Message)>
        GetAllInsuranceAddonTypes()
    {
        _logger.LogInformation("Controller activated to list all insurance addon types...");
        var result = await _insuranceListingService.GetAllInsuranceAddonTypes();

        if (result.success)
        {
            _logger.LogInformation($"Insurance addon types retrieved successfully:\n{result.message}");
            return (true, result.insuranceAddonTypes, result.message);
        }

        _logger.LogWarning($"Error retrieving insurance addon types: {result.message}");
        return (false, new List<InsuranceAddonType>(), result.message);
    }

    // Controller for GetAllInsurancePolicyHolders method
    public async Task<(List<InsurancePolicyHolder> insurancePolicyHolders, string message)>
        GetAllInsurancePolicyHolders()
    {
        _logger.LogInformation("Controller activated to list all insurance policy holders...");
        var result = await _insuranceListingService.GetAllInsurancePolicyHolders();

        if (result.success)
        {
            _logger.LogInformation($"Insurance policy holders retrieved successfully:\n{result.message}");
            return (result.insurancePolicyHolders, result.message);
        }

        _logger.LogWarning($"Error retrieving insurance policy holders: {result.message}");
        return (new List<InsurancePolicyHolder>(), result.message);
    }

    // Controller for GetAllLiabilityCoverageOptions method
    public async Task<(List<LiabilityCoverageOption> liabilityCoverageOptions, string message)>
        GetAllLiabilityCoverageOptions()
    {
        _logger.LogInformation("Controller activated to list all liability coverages...");
        var result = await _insuranceListingService.GetAllLiabilityCoverageOptions();

        if (result.success)
        {
            _logger.LogInformation($"Liability coverages retrieved succesfully:\n{result.message}");
            return (result.liabilityCoverageOptions, result.message);
        }

        _logger.LogWarning($"Error retrieving liability coverages: {result.message}");
        return (new List<LiabilityCoverageOption>(), result.message);
    }

    // Controller for GetAllRiskZones method
    public async Task<(List<RiskZone> riskzones, string message)> GetAllRiskZones()
    {
        _logger.LogInformation("Controller activated to list all risk zones...");
        var result = await _insuranceListingService.GetAllRiskZones();

        if (result.success)
        {
            _logger.LogInformation($"Risk zones retrieved successfully:\n{result.message}");
            return (result.riskZones, result.message);
        }

        _logger.LogWarning($"Error retrieving risk zones: {result.message}");
        return (new List<RiskZone>(), result.message);
    }

    // Controller for GetAllPropertyAndInventoryCoverages method
    public async Task<(List<PropertyAndInventoryCoverage> propertyAndInventoryCoverages, string message)>
        GetAllPropertyAndInventoryCoverages()
    {
        _logger.LogInformation("Controller activated to list all property and inventory coverages...");
        var result = await _insuranceListingService.GetAllPropertyAndInventoryCoverages();

        if (result.success)
        {
            _logger.LogInformation($"Property and inventory coverages retrieved successfully:\n{result.message}");
            return (result.propertyAndInventoryCoverages, result.message);
        }

        _logger.LogWarning($"Error retrieving property and inventory coverages: {result.message}");
        return (new List<PropertyAndInventoryCoverage>(), result.message);
    }

    // Controller for GetSpecificPrivateOption method
    public async Task<(List<PrivateCoverageOption> PrivateCoverageOptions, string Message)> GetSpecificPrivateOption(
        InsuranceType insuranceType)
    {
        _logger.LogInformation("Controller activated to get specific private option...");
        var result = await _insuranceListingService.GetSpecificPrivateOption(insuranceType);

        if (result.Success)
        {
            _logger.LogInformation($"Private option found:\n{result.Message}");
            return (result.PrivateCoverageOptions, result.Message);
        }

        _logger.LogWarning($"Error getting private option: {result.Message}");
        return (new List<PrivateCoverageOption>(), result.Message);
    }
}