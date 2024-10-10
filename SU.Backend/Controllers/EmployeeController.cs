using Microsoft.Extensions.Logging;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Controllers
{
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public async Task CreateRandomNewEmployee(EmployeeType Role)
        {
            _logger.LogInformation($"Controller activated to create new random {Role.ToString()}...");
            var result = await _employeeService.GenerateRandomEmployee(Role);

            if (result.Success)
            {
                _logger.LogInformation($"Employee created successfully:\n{result.Employee}");
            }
            else
            {
                _logger.LogWarning($"Error creating employee: {result.Message}");
            }
        }

        public async Task ListAllEmployees(Employee employee)
        {
            _logger.LogInformation("Controller activated to list all employees...");
            var result = await _employeeService.ListAllEmployees();

            if (result.Success)
            {
                _logger.LogInformation($"Employees retrieved successfully:\n{result.Employees}");
            }
            else
            {
                _logger.LogWarning($"Error retrieving employees: {result.Message}");
            }
        }
        
    }
}
