using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU.Backend.Configuration;
using SU.Backend.Controllers;
using SU.Backend.Models.Enums;
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
        //await employeeController.CreateRandomNewEmployee(EmployeeType.OutsideSales);

        ///Test PrivateCustomerController
        var privateCustomerController = host.Services.GetRequiredService<PrivateCustomerController>();
        //await privateCustomerController.GenerateRandomPrivateCustomer();

        ///Test LoginController
        //var loginController = host.Services.GetRequiredService<LoginController>();
        //await loginController.Authentication("cene", "zigzag");

        ///Test InsuranceService
        var insuranceService = host.Services.GetRequiredService<IInsuranceService>();
        //await insuranceService.CreateTestInsurance();
        //await insuranceService.RemoveAllInsurances(); 

        var prospectService = host.Services.GetRequiredService<IProspectService>();
        //await prospectService.IdentifyProspects();
        //await prospectService.TestAssignSellerToProspect();

    }

}
