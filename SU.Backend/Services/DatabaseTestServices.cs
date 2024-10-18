using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Database.Repositories;
using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    /// <summary>
    /// This class is responsible for handling the business logic for database testing.
    /// Not used for final application, only for testing purposes.
    /// </summary>
    public class DatabaseTestServices : IDatabaseTestService
    {
        private readonly ILogger<DatabaseTestServices> _logger;
        private readonly Context _context;
        private readonly UnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;



        public DatabaseTestServices(ILogger<DatabaseTestServices> logger, Context context,UnitOfWork unitOfWork, IEmployeeService employeeService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _context = context;
            _employeeService = employeeService;
        }


        public async Task<(bool Success, string Message)> RecreateDb()
        {
            _logger.LogInformation("Recreating database");

            try
            {
                var created = await _context.Database.EnsureCreatedAsync();
                return (created, "Database recreated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while recreating the database");
                return (false, "An error occurred while recreating the database");
            }
        }

        public async Task<(bool Success, string Message)> SeedEmployeeOrganisation()
        {
            _logger.LogInformation("Seeding employee organisation according to business description");
            _logger.LogInformation("Seeding CEO, Financial Manager, Sales Assistant and Financial Assistant");

            try
            {
                #region CEO SEED
                var newEmployee = new Employee
                {
                    FirstName = "Sten",
                    LastName = "Hård",
                    Email = EmployeeHelper.GenerateEmployeeEmail("Sten", "Hård"),
                    Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Sten", Last = "Hård" }),
                    Password = EmployeeHelper.GenerateEmployeePassword("Sten", "Hård"),
                    BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.CEO),
                    Manager = await _employeeService.GetManagerForRole(EmployeeType.CEO)
                };

                // Rolltilldelning för CEO
                var ceoRoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Employee = newEmployee,
                        Role = EmployeeType.CEO,
                        Percentage = 100
                    }
                };

                await _unitOfWork.Employees.AddAsync(newEmployee);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New CEO added successfully: {EmployeeName}", newEmployee.FirstName + " " + newEmployee.LastName);
                #endregion

                #region Sales Manager SEED
                var newEmployee2 = new Employee
                {
                    FirstName = "Iren",
                    LastName = "Panik",
                    Email = EmployeeHelper.GenerateEmployeeEmail("Iren", "Panik"),
                    Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Iren", Last = "Panik" }),
                    Password = EmployeeHelper.GenerateEmployeePassword("Iren", "Panik"),
                    BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.SalesManager),
                    Manager = await _employeeService.GetManagerForRole(EmployeeType.SalesManager),
                };

                var salesManagerRoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Employee = newEmployee2,
                        Role = EmployeeType.SalesManager,
                        Percentage = 100
                    }
                };

                await _unitOfWork.Employees.AddAsync(newEmployee);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New Sales Manager added successfully: {EmployeeName}", newEmployee.FirstName + " " + newEmployee.LastName);
                #endregion

                #region Sales Assistant SEED
                var newEmployee3 = new Employee
                {
                    FirstName = "Karin",
                    LastName = "Sundberg",
                    Email = EmployeeHelper.GenerateEmployeeEmail("Karin", "Sundberg"),
                    Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Karin", Last = "Sundberg" }),
                    Password = EmployeeHelper.GenerateEmployeePassword("Karin", "Sundberg"),
                    BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.InsideSales), // InsideSales base salary since it's majority percentage
                    Manager = await _employeeService.GetManagerForRole(EmployeeType.InsideSales),
                };

                var salesAssistantRoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Employee = newEmployee3,
                        Role = EmployeeType.SalesAssistant,
                        Percentage = 25
                    },
                    new EmployeeRoleAssignment
                    {
                        Employee = newEmployee3,
                        Role = EmployeeType.InsideSales,
                        Percentage = 75
                    }
                };

                await _unitOfWork.Employees.AddAsync(newEmployee3);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New Sales Assistant added successfully: {EmployeeName}", newEmployee3.FirstName + " " + newEmployee3.LastName);
                #endregion

                #region Financial Assistant SEED
                var newEmployee4 = new Employee
                {
                    FirstName = "Ann-Sofie",
                    LastName = "Larsson",
                    Email = EmployeeHelper.GenerateEmployeeEmail("Ann-Sofie", "Larsson"),
                    Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Ann-Sofie", Last = "Larsson" }),
                    Password = EmployeeHelper.GenerateEmployeePassword("Ann-Sofie", "Larsson"),
                    BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.FinancialAssistant),
                    Manager = await _employeeService.GetManagerForRole(EmployeeType.FinancialAssistant),
                };

                var financialAssistantRoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Employee = newEmployee4,
                        Role = EmployeeType.FinancialAssistant,
                        Percentage = 100
                    }
                };

                await _unitOfWork.Employees.AddAsync(newEmployee4);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("New Financial Assistant added successfully: {EmployeeName}", newEmployee4.FirstName + " " + newEmployee4.LastName);
                #endregion


                return (true, "Employee organisation seeded successfully");
            }
           
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new employee.");
                return (false, "An error occurred while adding a new employee.");
            }



        }

        public async Task<(bool Success, string Message)> TestDbConnection()
        {
            _logger.LogInformation("Testing database connection");

            try
            {
                if (await _context.Database.CanConnectAsync())
                {
                    _logger.LogInformation("Database connection successful");
                    return (true, "Database connection successful");
                }
                else
                {
                    _logger.LogWarning("Database connection failed");
                    return (false, "Database connection failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while testing the database connection");
                return (false, "An error occurred while testing the database connection");
            }
        }

        
    }
}

