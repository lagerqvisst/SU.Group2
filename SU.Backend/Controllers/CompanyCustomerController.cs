using Microsoft.Extensions.Logging;
using SU.Backend.Models.Customers;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    /// <summary>
    /// This class is responsible for handling the company customer controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class CompanyCustomerController
    {
        private readonly ICompanyCustomerService _companyCustomerService;
        private readonly ILogger<CompanyCustomerController> _logger;
        public CompanyCustomerController(ICompanyCustomerService companyCustomerService, ILogger<CompanyCustomerController> logger)
        {
            _companyCustomerService = companyCustomerService;
            _logger = logger;
        }

        public async Task<(bool success, string message)> CreateCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Controller activated to create new company customer...");
            var result = await _companyCustomerService.CreateCompanyCustomer(companyCustomer);

            if (result.success)
            {
                _logger.LogInformation($"{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"{result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Controller activated to update company customer...");
            var result = await _companyCustomerService.UpdateCompanyCustomer(companyCustomer);

            if (result.success)
            {
                _logger.LogInformation($"{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"{result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool success, string message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Controller activated to delete company customer...");
            var result = await _companyCustomerService.DeleteCompanyCustomer(companyCustomer);

            if (result.success)
            {
                _logger.LogInformation($"{result.message}");
                return (result.success, result.message);
            }
            else
            {
                _logger.LogWarning($"{result.message}");
                return (result.success, result.message);
            }
        }

        public async Task<(bool Success, string Message, CompanyCustomer? Customer)> GetCompanyCustomerById(int id)
        {
            _logger.LogInformation("Controller activated to retrieve company customer by ID...");
            var result = await _companyCustomerService.GetCompanyCustomerById(id);

            if (result.Success)
            {
                _logger.LogInformation($"{result.Message}");
                return (result.Success, result.Message, result.Customer);
            }
            else
            {
                _logger.LogWarning($"{result.Message}");
                return (result.Success, result.Message, result.Customer);
            }
        }

        public async Task<(List<CompanyCustomer> CompanyCustomers, string Message)> GetAllCompanyCustomers()
        {
            _logger.LogInformation("Controller activated to list all company customers...");
            var result = await _companyCustomerService.GetAllCompanyCustomers();
            if (result.Success)
            {
                _logger.LogInformation($"Company customers retrieved successfully:\n{result.Message}");
                return (result.CompanyCustomers, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving company customers: {result.Message}");
                return (new List<CompanyCustomer>(), result.Message);
            }
        }

    }
}