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

        public async Task<(bool Success, string Message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Controller activated to update private customer...");
            var result = await _privateCustomerService.UpdatePrivateCustomer(privateCustomer);

            if (result.Success)
            {
                _logger.LogInformation($"The private custimer has been succesfully updated {result.Customer}");
                return (result.Success, result.Message);
            }
            else
            {
                _logger.LogWarning($"An error has occured and the private customer could not be updated {result.Message}");
                return (result.Success, result.Message);
            }
        }

    }
}
