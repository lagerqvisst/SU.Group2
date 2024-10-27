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
    /// <summary>
    /// This class is responsible for handling the employee controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        //Only for testing purposes
        public async Task CreateRandomNewEmployee(EmployeeType role)
        {
            _logger.LogInformation($"Controller activated to create new random {role.ToString()}...");
            var result = await _employeeService.GenerateRandomEmployee(role);

            if (result.success)
            {
                _logger.LogInformation($"Employee created successfully:\n{result.employee}");
            }
            else
            {
                _logger.LogWarning($"Error creating employee: {result.message}");
            }
        }

        public async Task <(bool success, string message, List <Employee>)> GetAllEmployees()
        {
            _logger.LogInformation("Controller activated to list all employees...");
            var result = await _employeeService.GetAllEmployees();

            if (result.success)
            {
                _logger.LogInformation($"Employees retrieved successfully:\n{result.employees.Count}");
                return (result.success, result.message, result.employees);
            }
            else
            {
                _logger.LogWarning($"Error retrieving employees: {result.message}");
                return (result.success, result.message, null);
            }
        }
        //Controller for add employee
        public async Task<(bool success, string message)> CreateEmployee(Employee employee)
        {
            _logger.LogInformation("Controller activated to create new employee...");
            var result = await _employeeService.CreateNewEmployee(employee);

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
        //Controller for delete employee
        public async Task<(bool success, string message)> DeleteEmployee(Employee employee)
        {
            _logger.LogInformation("Employee object updated via GUI");
            var result = await _employeeService.DeleteEmployee(employee);

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
        //Controller for update employee
        public async Task<(bool success, string message)> UpdateEmployee(Employee employee)
        {
            _logger.LogInformation("Employee object updated via GUI");
            var result = await _employeeService.UpdateEmployee(employee);

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

        public async Task<(List<EmployeeRoleAssignment> employeeRoleAssignments, string message)> GetAllEmployeeRoleAssignments()
        {
            _logger.LogInformation("Controller activated to list all employee role assignments...");
            var result = await _employeeService.GetAllEmployeeRoleAssignments();

            if (result.success)
            {
                _logger.LogInformation($"Employee role assignments found:\n{result.message}");
                return (result.employeeRoleAssignments, result.message);
            }
            else
            {
                _logger.LogWarning($"Error retrieving employee role assignments: {result.message}");
                return (new List <EmployeeRoleAssignment>(), result.message);
            }
        }

        public async Task<(bool success, string message, Employee employee)> GetEmployeeByRole(EmployeeType role)
        {
            _logger.LogInformation("Controller activated to get employee by role...");
            var result = await _employeeService.GetEmployeeByRole(role);

            if (result.success)
            {
                _logger.LogInformation($"Employee found:\n{result.employee}");
                return (result.success, result.message, result.employee);
            }
            else
            {
                _logger.LogWarning($"Error retrieving employee: {result.message}");
                return (result.success, result.message, null);
            }
        }
    }
}
