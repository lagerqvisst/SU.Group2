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
    public class CompanyCustomerController
    {
        private readonly ICompanyCustomerService _companyCustomerService;
        private readonly ILogger<CompanyCustomerController> _logger;
        public CompanyCustomerController(ICompanyCustomerService companyCustomerService, ILogger<CompanyCustomerController> logger)
        {
            _companyCustomerService = companyCustomerService;
            _logger = logger;
        }

        public async Task<(bool Success, string Message)> CreateCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Controller activated to create new company customer...");
            var result = await _companyCustomerService.CreateCompanyCustomer(companyCustomer);

            if (result.Success)
            {
                _logger.LogInformation($"{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"{result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(bool Success, string Message)> UpdateCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Controller activated to update company customer...");
            var result = await _companyCustomerService.UpdateCompanyCustomer(companyCustomer);

            if (result.Success)
            {
                _logger.LogInformation($"{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"{result.Message}");
                return (result.Success, result.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteCompanyCustomer(CompanyCustomer companyCustomer)
        {
            _logger.LogInformation("Controller activated to delete company customer...");
            var result = await _companyCustomerService.DeleteCompanyCustomer(companyCustomer);

            if (result.Success)
            {
                _logger.LogInformation($"{result.Message}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"{result.Message}");
                return (result.Success, result.Message);
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

        public async Task<(List<CompanyCustomer> CompanyCustomers, string Message)> ListAllCompanyCustomers()
        {
            _logger.LogInformation("Controller activated to list all company customers...");
            var result = await _companyCustomerService.ListAllCompanyCustomers();
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