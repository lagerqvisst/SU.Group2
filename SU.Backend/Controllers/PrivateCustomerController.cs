﻿using Microsoft.Extensions.Logging;
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
    /// This class is responsible for handling the private customer controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
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

            if (result.success)
            {
                _logger.LogInformation($"Customer created successfully:\n{result.customer}");
            }
            else
            {
                _logger.LogWarning($"Error creating customer: {result.message}");
            }
        }

        // controller for CreateNewPrivateCustomer method 
        public async Task<(bool Success, String Message)> CreateNewPrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Private Customer object added via GUI");
            var result = await _privateCustomerService.CreateNewPrivateCustomer(privateCustomer);

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

        // controller for UpdatePrivateCustomer method
        public async Task<(bool Success, string Message)> UpdatePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Private Customer object updated via GUI");
            var result = await _privateCustomerService.UpdatePrivateCustomer(privateCustomer);

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

        // controller for DeletePrivateCustomer method 
        public async Task<(bool Success, string Message)> DeletePrivateCustomer(PrivateCustomer privateCustomer)
        {
            _logger.LogInformation("Private Customer deleted via GUI");
            var result = await _privateCustomerService.DeletePrivateCustomer(privateCustomer);

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


        public async Task<(List<PrivateCustomer> PrivateCustomers, string Message)> GetAllPrivateCustomers()
        {
            _logger.LogInformation("Controller activated to list all private customers...");
            var result = await _privateCustomerService.GetAllPrivateCustomers();
            if (result.success)
            {
                _logger.LogInformation($"Private customers retrieved successfully:\n{result.message}");
                return (result.privateCustomers, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving private customers: {result.message}");
                return (new List<PrivateCustomer>(), result.message);
            }
        }
    }
}


