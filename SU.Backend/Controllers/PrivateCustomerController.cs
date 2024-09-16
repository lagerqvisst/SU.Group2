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

        public PrivateCustomerController(IPrivateCustomerService privateCustomerService)
        {
            _privateCustomerService = privateCustomerService;
        }

        public async Task GenerateRandomPrivateCustomer()
        {
            var result = await _privateCustomerService.GenerateRandomPrivateCustomer();

            if (result.Success)
            {
                Console.WriteLine($"Private customer created successfully:\n{result.Customer}");
            }
            else
            {
                Console.WriteLine($"Error creating customer: {result.Message}");
            }
        }
    }
}
