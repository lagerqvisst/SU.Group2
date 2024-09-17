using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Employee;
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
                        Percentage = 100
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
        private async Task<Employee?> GetManagerForRole(EmployeeType role)
        {
            switch (role)
            {
                case EmployeeType.OutsideSales:
                case EmployeeType.InsideSales:
                case EmployeeType.SalesAssistant:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.SalesManager);

                case EmployeeType.FinancialAssistant:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.FinancialManager);

                case EmployeeType.SalesManager:
                case EmployeeType.FinancialManager:
                    return await _unitOfWork.Employees.GetEmployeeByRole(EmployeeType.CEO);

                default:
                    return null;
            }
        }
    }
}
