using Microsoft.Extensions.Logging;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Services;
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

        public async Task <(bool Success, string Message, List <Employee>)> GetAllEmployees()
        {
            _logger.LogInformation("Controller activated to list all employees...");
            var result = await _employeeService.GetAllEmployees();

            if (result.Success)
            {
                _logger.LogInformation($"Employees retrieved successfully:\n{result.Employees.Count}");
                return (result.Success, result.Message, result.Employees);
            }
            else
            {
                _logger.LogWarning($"Error retrieving employees: {result.Message}");
                return (result.Success, result.Message, null);
            }
        }
        //Controller for add employee
        public async Task<(bool Success, string Message)> CreateEmployee(Employee employee)
        {
            _logger.LogInformation("Controller activated to create new employee...");
            var result = await _employeeService.CreateNewEmployee(employee);

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
        //Controller for delete employee
        public async Task<(bool Success, string Message)> DeleteEmployee(Employee employee)
        {
            _logger.LogInformation("Employee object updated via GUI");
            var result = await _employeeService.DeleteEmployee(employee);

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
        //Controller for update employee
        public async Task<(bool Success, string Message)> UpdateEmployee(Employee employee)
        {
            _logger.LogInformation("Employee object updated via GUI");
            var result = await _employeeService.UpdateEmployee(employee);

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

        public async Task<(List<EmployeeRoleAssignment> EmployeeRoleAssignments, string Message)> ListAllEmployeeRoleAssignments()
        {
            _logger.LogInformation("Controller activated to list all employee role assignments...");
            var result = await _employeeService.ListAllEmployeeRoleAssignments();

            if (result.Success)
            {
                _logger.LogInformation($"Employee role assignments found:\n{result.Message}");
                return (result.EmployeeRoleAssignments, result.Message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving employee role assignments: {result.Message}");
                return (new List <EmployeeRoleAssignment>(), result.Message);
            }
        }
    }
}
