using SU.Backend.Helper;
using SU.Backend.Models;
using SU.Backend.Models.Enums;
using SU.Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRandomGenerationService _randomInfoGenerationService;

        public EmployeeService(IRandomGenerationService randomInfoGenerationService)
        {
            _randomInfoGenerationService = randomInfoGenerationService;
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
                        BaseSalary = Employee.GetSalaryForEmployeeType(Role),
                        //TODO: Update AgentNumberGenerator method with service to check if agent number is already in use.
                        AgentNumber = Role.Equals(EmployeeType.OutsideSales) ? AgentNumberGenerator.GenerateFourDigitCode() : null
                        //TODO: Service to check for who should be assigned manager depending on role. Needs DB implementation.
                    };

                    return (true, "Successfully generated new employee", employee);
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
    }
}
