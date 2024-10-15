using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU.Backend.Configuration;
using SU.Backend.Controllers;
using SU.Backend.Database;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddBackendServices();
            })
            .Build();

        UnitOfWork unitOfWork = new UnitOfWork(host.Services.GetRequiredService<Context>());

        #region DbTests
        ///Test DatabaseTestService
        var databaseTestService = host.Services.GetRequiredService<IDatabaseTestService>();
        //await databaseTestService.SeedEmployeeOrganisation();
        #endregion

        #region EmployeeTests
        ///Test EmplyeeController
        
        var employeeController = host.Services.GetRequiredService<EmployeeController>();
        var employeeService = host.Services.GetRequiredService<IEmployeeService>();
        //await employeeController.CreateRandomNewEmployee(EmployeeType.OutsideSales);
        /*
        var newEmployee = new Employee
        {
            PersonalNumber = "1234XD",
            FirstName = "Test",
            LastName = "Test",
            Email = "Test",
            BaseSalary = 0,
            Username = "Test",
            Password = "Test",

        };

        var roleassignement = new EmployeeRoleAssignment
        {
            Role = EmployeeType.FinancialAssistant,
            Percentage = 100

        };

        newEmployee.RoleAssignments.Add(roleassignement);
        await employeeController.CreateEmployee(newEmployee);*/
        #endregion

        #region Customer tests
        ///Test PrivateCustomerController
        var privateCustomerController = host.Services.GetRequiredService<PrivateCustomerController>();
        //await privateCustomerController.GenerateRandomPrivateCustomer();

       


        //Test CompanyCustomerService
        var companyCustomerService = host.Services.GetRequiredService<ICompanyCustomerService>();
        //var CompanyCustomerController = host.Services.GetRequiredService<CompanyCustomerController>();

        //await companyCustomerService.GenerateTestCompanyCustomer();


       /* var newCompanyCustomer = new CompanyCustomer
        {
            // CompanyCustomerID generates automatically
            CompanyName = "TestCompany",
            OrganizationNumber = "123456-7890",
            ContactPerson = "TestContact",
            ContactPersonPhonenumber = "070-1234567",
            CompanyAdress = "TestStreet 1, 12345 TestCity",
            CompanyPhoneNumber = "08-123456",
            CompanyLandlineNumber = "08-7654321",
            CompanyEmailAdress = "dsad"
        };*/

        //await CompanyCustomerController.CreateCompanyCustomer(newCompanyCustomer);
        
        //Enter specific ID to get a specific CompanyCustomer to either update or delete.
        //var CompanyCustomers = await companyCustomerService.GetCompanyCustomerById(4);

        //Update specific CompanyCustomer
        /*CompanyCustomers.Customer.CompanyName = "Peps Persson";
        await CompanyCustomerController.UpdateCompanyCustomer(CompanyCustomers.Customer);*/

        //Delete specific CompanyCustomer
        //await CompanyCustomerController.DeleteCompanyCustomer(CompanyCustomers.Customer);

        #endregion

        #region Login Tests
        ///Test LoginController
        var loginController = host.Services.GetRequiredService<LoginController>();
        //await loginController.Authentication("cene", "zigzag");
        #endregion

        #region Insurance tests
        ///Test InsuranceService
        var insuranceService = host.Services.GetRequiredService<IInsuranceService>();
        var insuranceController = host.Services.GetRequiredService<InsuranceController>();
        var privateCoverageService = host.Services.GetRequiredService<IPrivateCoverageService>();

        //Test remove
        //var insuranceToDelete = await unitOfWork.Insurances.GetInsuranceById(6);
        //await insuranceService.DeleteInsurance(insuranceToDelete);

        //Test property and inventory

        /*var propertyCoverage = new PropertyAndInventoryCoverage(1000000, 500000)
        {
            PropertyAddress = "TestStreet 1, 12345 TestCity"
        };*/
        //Test company customer 
        //var companyCustomer = await companyCustomerService.GetCompanyCustomerById(1);
        //var seller = await employeeService.GetEmployeeById(1);
        //await insuranceController.CreatePropertyInventoryInsurance(companyCustomer.Customer, propertyCoverage, seller.Employee, "TestNote");

        //Test customer
        /*
        var customer = unitOfWork.PrivateCustomers.GetPrivateCustomers().Result.First();
        //Test option
        var privateCoverageOption = privateCoverageService.GetPrivateCoverageOptionAsync(750000, InsuranceType.ChildAccidentAndHealthInsurance);
        //Test seller 
        var seller = await employeeService.GetEmployeeById(1);

        //Om vi vill ha typ en checkbox för att enkelt säga att förskrad person är samma som försäkringstagare
        bool isPolicyHolderInsured = true;

        var insuranceType = InsuranceType.AdultLifeInsurance;


        await insuranceController.CreatePrivateInsurance(customer, insuranceType, privateCoverageOption.Result.CoverageOption, seller.Employee, isPolicyHolderInsured);
        */


        //await insuranceService.CreateTestPrivateInsurance();
        //await insuranceService.CreateCompanyInsurance();
        //await insuranceService.CreateCompanyInsuranceProperty();
        await insuranceService.CreateTestCompanyLiability();
        //await insuranceService.RemoveAllInsurances(); 
        #endregion

        #region Prospect tests
        var prospectService = host.Services.GetRequiredService<IProspectService>();
        //await prospectService.IdentifyProspects();
        //await prospectService.TestAssignSellerToProspect();
        #endregion

        #region Statistics tests
        var commissionService = host.Services.GetRequiredService<ICommissionService>();
        //await commissionService.GetAllCommissions(startDate, endDate);

        //Tester för Statistikkravet i sista bilagan.
        /*
        var statisticsService = host.Services.GetRequiredService<IStatisticsService>();
        var statistics = await statisticsService.GetSellerStatistics(2024);

        List<InsuranceType> insuranceTypes = new List<InsuranceType> { InsuranceType.ChildAccidentAndHealthInsurance, 
                                                                       InsuranceType.AdultAccidentAndHealthInsurance,
                                                                       InsuranceType.AdultLifeInsurance};

        statisticsService.PrintSellerStatistics(statistics, 2024, insuranceTypes);*/
        #endregion

    }

}
