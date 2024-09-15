using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models;
using SU.Backend.Models.Enums;
using SU.Backend.Services.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRandomGenerationService _randomInfoGenerationService;
        private readonly UnitOfWork _unitOfWork; // Lägg till UnitOfWork

        public EmployeeService(IRandomGenerationService randomInfoGenerationService, UnitOfWork unitOfWork)
        {
            _randomInfoGenerationService = randomInfoGenerationService;
            _unitOfWork = unitOfWork; // Injicera UnitOfWork
        }

        public async Task<(bool Success, string Message, Employee Employee)> GenerateRandomEmployee(EmployeeType Role)
        {
            try
            {
                var (success, randomUser) = await _randomInfoGenerationService.GenerateRandomCustomer();

                if (success)
                {
                    var info = randomUser.Results[0];


                    Employee employee = new Employee
                    {
                        FirstName = info.Name.First,
                        LastName = info.Name.Last,
                        Email = info.Email,
                        Role = Role,
                        Username = info.Login.Username,
                        Password = info.Login.Password,
                        Manager = await GetManagerForRole(Role),
                        BaseSalary = Employee.GetSalaryForEmployeeType(Role),
                        AgentNumber = (Role == EmployeeType.OutsideSales || Role == EmployeeType.InsideSales) ? AgentNumberGenerator.GenerateFourDigitCode() : null
                        
                    };

                    // Spara den nya anställda i databasen via UnitOfWork
                    _unitOfWork.Employees.Add(employee);
                    _unitOfWork.SaveChanges(); // Spara ändringar till databasen

                    return (true, "Successfully generated and saved new employee", employee);
                }
                else
                {
                    return (false, "Failed to generate random employee", null);
                }
            }
            catch (Exception ex)
            {
                return (false, "An error occurred: " + ex.Message, null);
            }
        }

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
