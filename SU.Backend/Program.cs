using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU.Backend.Configuration;
using SU.Backend.Controllers;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Services.Interfaces;
using System;
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



        ///Test DatabaseTestService
        var databaseTestService = host.Services.GetRequiredService<IDatabaseTestService>();
        //await databaseTestService.SeedEmployeeOrganisation();

        ///Test EmplyeeController
        var employeeController = host.Services.GetRequiredService<EmployeeController>();
        //await employeeController.CreateRandomNewEmployee(EmployeeType.InsideSales);

        ///Test PrivateCustomerController
        var privateCustomerController = host.Services.GetRequiredService<PrivateCustomerController>();
        //await privateCustomerController.GenerateRandomPrivateCustomer();

        //Test CompanyCustomerService
        var companyCustomerService = host.Services.GetRequiredService<ICompanyCustomerService>();
        //await companyCustomerService.GenerateTestCompanyCustomer();

        ///Test LoginController
        //var loginController = host.Services.GetRequiredService<LoginController>();
        //await loginController.Authentication("cene", "zigzag");

        ///Test InsuranceService
        var insuranceService = host.Services.GetRequiredService<IInsuranceService>();
        //await insuranceService.CreateTestInsurance();
        await insuranceService.CreateCompanyInsurance();
        //await insuranceService.RemoveAllInsurances(); 

        var prospectService = host.Services.GetRequiredService<IProspectService>();
        //await prospectService.IdentifyProspects();
        //await prospectService.TestAssignSellerToProspect();

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

        //Alex was here
        //Kasper was here
        // Adam W was here for a second time since my first try didnt work!
        // Adam Å was here :)
        // emily was here


    }

}
