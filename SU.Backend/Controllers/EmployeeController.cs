using Microsoft.Extensions.Logging;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
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
    /// This class is responsible for handling the employee controller.
    /// Makes logic available in the Viewmodel
    /// More info about the logic for each method can be found in the Service function each controller method uses.
    /// </summary>
    public class EmployeeController
    {
        // Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        // Constructor
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        //Controller for GetAllEmployees method
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
        //Controller for CreateEmployee method
        public async Task<(bool success, string message, Employee employee)> CreateEmployee(EmployeeType role, string firstName, string lastName, string personalNumber)
        {
            _logger.LogInformation("Controller activated to create new employee...");
            var result = await _employeeService.CreateNewEmployee(role, firstName, lastName, personalNumber); 

            if (result.success)
            {
                _logger.LogInformation($"{result.message}");
                return(result.success, result.message, result.employee);
            }
            else
            {
                _logger.LogWarning($"{result.message}");
                return (result.success, result.message, result.employee);
            }
        }

        //Controller for DeleteEmployee method
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

        //Controller for UpdateEmployee method
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

        //Controller for GetAllEmployeeRoleAssignments method
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

        //Controller for GetEmployeeByRole method
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

        //Controller for GetAllSellers method
        public async Task<(bool success, string message, List<Employee> salesEmployees)> GetAllSellers()
        {
            _logger.LogInformation("Controller activated to list all sales employees...");

            var result = await _employeeService.GetAllSalesEmployees();

            if (result.success)
            {
                _logger.LogInformation($"Sales employees found:\n{result.salesEmployees.Count}");
                return (result.success, result.message, result.salesEmployees);
            }
            else
            {
                _logger.LogWarning($"Error retrieving sales employees: {result.message}");
                return (result.success, result.message, null);
            }
        }
    }
}