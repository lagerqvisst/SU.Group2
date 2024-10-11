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

        //Auto assign manager based on role
        //Kanske kan bli knas när vi har anställda som har fler roller såsom Sales Assistant & Sales för fallet Karin.
        //Kanske skapa en service metod för employeeservice där vi bygger en repositoryklass mot EmployeeRoleAssignments. 
        //Då kan göra ett case där om en anställd har fler än 1 roll, så tar man den rollen som har högst procent och sätter som manager.
        public async Task<Employee?> GetManagerForRole(EmployeeType role)
        {
            switch (role)
            {
                case EmployeeType.OutsideSales:
                case EmployeeType.InsideSales:
                case EmployeeType.SalesAssistant:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.SalesManager); //Iren Panik

                //Dessa två svarar till VD Stenhård
                case EmployeeType.FinancialAssistant:
                case EmployeeType.SalesManager:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.CEO);

                default:
                    return null;
            }
        }

        public async Task<(bool Success, string Message, List<Employee?> Employees)> ListAllEmployees()
        {
            _logger.LogInformation("Getting all employees");
            try
            {
                var employees = await _unitOfWork.Employees.ListAllEmployees();
                if (employees == null || !employees.Any())
                {
                    _logger.LogInformation("No employees found");
                    return (false, "No employees found", null);
                }
                _logger.LogInformation($"Found {employees.Count} employees");
                return (true, "Employees retrived succesfully", employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting employees");
                return (false, "Error getting employees", null);
            }
        }
        //method to add new Emplyee

        public async Task<(bool Success, string Message)> CreateNewEmployee(Employee employee)
        {
            _logger.LogInformation("Creating new Employee...");

            try
            {
                _logger.LogInformation("Attemptig to save to database...");
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Employee has been succesfully added to the database");

                return (true, "The new Employee customer was succesfully added to the system.");
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.ToString());
                return (false, $"There was an error saving the new user: {e.Message.ToString()}");
            }

        }
    }
}
