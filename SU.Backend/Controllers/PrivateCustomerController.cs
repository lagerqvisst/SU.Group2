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
    public class PrivateCustomerController
    {

        private readonly IPrivateCustomerService _privateCustomerService;
        private readonly ILogger<PrivateCustomerController> _logger;

        public PrivateCustomerController(IPrivateCustomerService privateCustomerService, ILogger<PrivateCustomerController> logger)
        {
            _privateCustomerService = privateCustomerService;
            _logger = logger;
        }

        public async Task GenerateRandomPrivateCustomer()
        {
            _logger.LogInformation("Controller activated to create new random private customer...");
            var result = await _privateCustomerService.GenerateRandomPrivateCustomer();

            if (result.Success)
            {
                _logger.LogInformation($"Customer created successfully:\n{result.Customer}");
            }
            else
            {
                _logger.LogWarning($"Error creating customer: {result.Message}");
            }
        }

        // controller for CreateNewPrivateCustomer method 
        public async Task<(bool Success, String Message)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Private Customer object added via GUI");
            var result = await _privateCustomerService.CreateNewPrivateCustomer(privateCustomer);

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

        // controller for UpdatePrivateCustomer method
        public async Task<(bool Success, string Message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Private Customer object updated via GUI");
            var result = await _privateCustomerService.UpdatePrivateCustomer(privateCustomer);

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

        // controller for DeletePrivateCustomer method 
        public async Task<(bool Success, string Message)> DeletePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Private Customer deleted via GUI");
            var result = await _privateCustomerService.DeletePrivateCustomer(privateCustomer);

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

        public async Task<(List<PrivateCustomer> PrivateCustomers, string Message)> PrivateCustomers()
        {
            _logger.LogInformation("Controller activated to list all private customers...");
            var result = await _privateCustomerService.ListAllPrivateCustomers();
            if (result.Success)
            {
                _logger.LogInformation($"Private customers retrieved successfully:\n{result.Message}");
                return (result.PrivateCustomers, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving private customers: {result.Message}");
                return (new List<PrivateCustomer>(), result.Message);
            }
        }
    }
}


