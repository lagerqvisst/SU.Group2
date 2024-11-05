using Microsoft.Extensions.Logging;
using SU.Backend.Models.Customers;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Controllers;

/// <summary>
///     This class is responsible for handling the company customer controller.
///     Makes logic available in the Viewmodel
///     More info about the logic for each method can be found in the Service function each controller method uses.
/// </summary>
public class CompanyCustomerController
{
    // Services
    private readonly ICompanyCustomerService _companyCustomerService;
    private readonly ILogger<CompanyCustomerController> _logger;

    // Constructor
    public CompanyCustomerController(ICompanyCustomerService companyCustomerService,
        ILogger<CompanyCustomerController> logger)
    {
        _companyCustomerService = companyCustomerService;
        _logger = logger;
    }

    // Controller for CreateCompanyCustomer method
    public async Task<(bool success, string message)> CreateCompanyCustomer(CompanyCustomer companyCustomer)
    {
        _logger.LogInformation("Controller activated to create new company customer...");
        var result = await _companyCustomerService.CreateCompanyCustomer(companyCustomer);

        if (result.success)
        {
            _logger.LogInformation($"{result.message}");
            return (result.success, result.message);
        }

        _logger.LogWarning($"{result.message}");
        return (result.success, result.message);
    }

    // Controller for UpdateCompanyCustomer method
    public async Task<(bool success, string message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer)
    {
        _logger.LogInformation("Controller activated to update company customer...");
        var result = await _companyCustomerService.UpdateCompanyCustomer(companyCustomer);

        if (result.success)
        {
            _logger.LogInformation($"{result.message}");
            return (result.success, result.message);
        }

        _logger.LogWarning($"{result.message}");
        return (result.success, result.message);
    }

    // Controller for DeleteCompanyCustomer method
    public async Task<(bool success, string message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer)
    {
        _logger.LogInformation("Controller activated to delete company customer...");
        var result = await _companyCustomerService.DeleteCompanyCustomer(companyCustomer);

        if (result.success)
        {
            _logger.LogInformation($"{result.message}");
            return (result.success, result.message);
        }

        _logger.LogWarning($"{result.message}");
        return (result.success, result.message);
    }

    // Controller for GetAllCompanyCustomers method
    public async Task<(List<CompanyCustomer> companyCustomers, string message)> GetAllCompanyCustomers()
    {
        _logger.LogInformation("Controller activated to list all company customers...");
        var result = await _companyCustomerService.GetAllCompanyCustomers();
        if (result.success)
        {
            _logger.LogInformation($"Company customers retrieved successfully:\n{result.message}");
            return (result.companyCustomers, result.message);
        }

        _logger.LogWarning($"Error retrieving company customers: {result.message}");
        return (new List<CompanyCustomer>(), result.message);
    }
}