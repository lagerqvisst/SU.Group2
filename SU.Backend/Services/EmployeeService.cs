using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Services.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling all operations related to employees.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private ILogger<EmployeeService> _logger;
        private readonly IRandomGenerationService _randomInfoGenerationService;
        private readonly UnitOfWork _unitOfWork; // Lägg till UnitOfWork

        public EmployeeService(IRandomGenerationService randomInfoGenerationService, UnitOfWork unitOfWork, ILogger<EmployeeService> logger)
        {
            _logger = logger;
            _randomInfoGenerationService = randomInfoGenerationService;
            _unitOfWork = unitOfWork; // Injicera UnitOfWork
        }

        // This method generates a random employee based on the provided role. Used for testing purposes.
        public async Task<(bool Success, string Message, Employee Employee)> GenerateRandomEmployee(EmployeeType Role)
        {
            _logger.LogInformation("Generating random employee");
            try
            {

                var (success, randomUser) = await _randomInfoGenerationService.GenerateSingleRandomUser();

                _logger.LogInformation("Random employee generated");
                if (success)
                {

                    var info = randomUser.Results[0];

                    _logger.LogInformation("Creating new employee object");
                    Employee employee = new Employee
                    {
                        FirstName = info.Name.First,
                        LastName = info.Name.Last,
                        PersonalNumber = "19900101-0000",
                        Email = info.Email,
                        Username = EmployeeHelper.GenerateEmployeeUsername(info.Name),
                        Password = info.Login.Password,
                        Manager = await GetManagerForRole(Role),
                        BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(Role),
                        AgentNumber = (Role == EmployeeType.OutsideSales || Role == EmployeeType.InsideSales) ? EmployeeHelper.GenerateFourDigitCode() : null,

                    };

                    _logger.LogInformation("Creating new role assignment object");
                    var roleAssignment = new EmployeeRoleAssignment
                    {
                        Employee = employee,
                        Role = Role,
                        Percentage = 10
                    };

                    employee.RoleAssignments.Add(roleAssignment);

                    _logger.LogInformation("New employee object created");
                    // Spara den nya anställda i databasen via UnitOfWork
                    _unitOfWork.Employees.Add(employee);
                    _unitOfWork.SaveChanges(); // Spara ändringar till databasen

                    _logger.LogInformation("New employee saved to database");
                    return (true, "Successfully generated and saved new employee", employee);
                }
                else
                {
                    _logger.LogWarning("Failed to generate random employee");
                    return (false, "Failed to generate random employee", null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating random employee");
                return (false, "An error occurred: " + ex.Message, null);
            }
        }

        //Auto assign manager based on role. In case an employee has multiple roles the highest role will be used (highest = highest %) 
        public async Task<Employee?> GetManagerForRole(EmployeeType role)
        {
            switch (role)
            {
                // These roles report to Sales Manager
                case EmployeeType.OutsideSales:
                case EmployeeType.InsideSales:
                case EmployeeType.SalesAssistant:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.SalesManager); //Iren Panik

                // These roles report to CEO
                case EmployeeType.FinancialAssistant:
                case EmployeeType.SalesManager:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.CEO);

                default:
                    return null;
            }
        }

        //Method to get all employees
        public async Task<(bool Success, string Message, List<Employee> Employees)> GetAllEmployees()
        {
            _logger.LogInformation("Controller activated to get all employees");
            try
            {
                var employees = await _unitOfWork.Employees.GetAllEmployees();
                _logger.LogInformation("Employees found: {EmployeesCount}", employees.Count);

                return (true, "Employees found", employees);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "Error occurred while fetching employees");
                return (false, "An error occurred while fetching the employees", new List<Employee>());
            }
        }

        //Method to add new Emplyee
        public async Task<(bool Success, string Message)> CreateNewEmployee(Employee employee)
        {
            _logger.LogInformation("Creating new Employee...");

            try
            {
                _logger.LogInformation("Attempting to save to database...");
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Employee has been successfully added to the database");

                return (true, "The new Employee customer was succesfully added to the system.");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.ToString());
                return (false, $"There was an error saving the new employee: {e.Message.ToString()}");
            }

        }

        //method to update new Employee
        public async Task<(bool Success, string Message)> UpdateEmployee(Employee employee)
        {
            _logger.LogInformation("Updating Employee...");

            try
            {
                _logger.LogInformation("Attempting to update a Employee...");

                await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Employee has been successfully updated.");

                return (true, "The Employee has been updated on the database.");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.ToString());
                return (false, $"An error has occurred while updating the employee: {e.Message.ToString()}");
            }
        }

        //Method to delete employee
        public async Task<(bool Success, string Message)> DeleteEmployee(Employee employee)
        {
            _logger.LogInformation("Deleting employee...");

            try
            {
                _logger.LogInformation("Attempting to delete a employee...");

                await _unitOfWork.Employees.RemoveAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Employee was successfully deleted.");

                return (true, "Employee was deleted the database.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return (false, $"An error occurred while deleting the employee: {ex.Message.ToString()}");

            }
        }

        //Method to get employee by id
        public async Task<(bool Success, string Message, Employee? Employee)> GetEmployeeById(int id)
        {
            _logger.LogInformation("Getting employee by id");

            try
            {
                var employee = await _unitOfWork.Employees.GetEmployeeById(id);

                if (employee == null)
                {
                    _logger.LogInformation("No employee found");
                    return (false, "No employee found", null);
                }

                _logger.LogInformation("Employee found");
                return (true, "Employee found", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employee");
                return (false, "Error getting employee", null);
            }
        }

        //Method to get all employee role assignments
        public async Task<(bool Success, string Message, List<EmployeeRoleAssignment> EmployeeRoleAssignments)> GetAllEmployeeRoleAssignments()
        {
            _logger.LogInformation("Controller activated to get all employee role assignments...");

            try
            {
                var employeeroleassignments = _unitOfWork.Employees.GetAllEmployeeRoleAssignments();
                _logger.LogInformation("Employee role assignments found: {EmployeeRoleAssignmentsCount}", employeeroleassignments.Result.Count);

                return (true, "Employee role assignments found.", employeeroleassignments.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employee role assignments.");
                return (false, "An error occurred while fetching the employee role assignments.", new List<EmployeeRoleAssignment>());
            }
        }

        public async Task<(bool Success, string Message, Employee Employee)> GetEmployeeByRole(EmployeeType role)
        {
            _logger.LogInformation("Getting employee by role");

            try
            {
                var employee = _unitOfWork.Employees.GetEmployeeByRole(role);
                if (employee == null)
                {
                    _logger.LogInformation("No employee found");
                    return (false, "No employee found", null);
                }

                _logger.LogInformation("Employee found");

                return (true, "Employee found", employee.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employee");
                return (false, "Error getting employee", null);

            }


        }
    }
}
