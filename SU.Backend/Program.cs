﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU.Backend.Configuration;
using SU.Backend.Controllers;
using SU.Backend.Models.Customers;
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
        //await employeeController.CreateRandomNewEmployee(EmployeeType.OutsideSales);

        ///Test PrivateCustomerController
        var privateCustomerController = host.Services.GetRequiredService<PrivateCustomerController>();
        //await privateCustomerController.GenerateRandomPrivateCustomer();

        //Test CompanyCustomerService
        var companyCustomerService = host.Services.GetRequiredService<ICompanyCustomerService>();
        var CompanyCustomerController = host.Services.GetRequiredService<CompanyCustomerController>();

        //await companyCustomerService.GenerateTestCompanyCustomer();


        /*var newCompanyCustomer = new CompanyCustomer
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
        
        //Enter specific ID to get a specific CompanyCustomer
        //var CompanyCustomers = await companyCustomerService.GetCompanyCustomerById(3);

        //Update specific CompanyCustomer
        /*CompanyCustomers.Customer.CompanyName = "TestCompany22";
        await CompanyCustomerController.UpdateCompanyCustomer(CompanyCustomers.Customer);*/

        //Delete specific CompanyCustomer
        //await CompanyCustomerController.DeleteCompanyCustomer(CompanyCustomers.Customer);

        ///Test LoginController
        var loginController = host.Services.GetRequiredService<LoginController>();
        //await loginController.Authentication("cene", "zigzag");

        ///Test InsuranceService
        var insuranceService = host.Services.GetRequiredService<IInsuranceService>();
        //await insuranceService.CreateTestPrivateInsurance();
        //await insuranceService.CreateCompanyInsurance();
        //await insuranceService.CreateCompanyInsuranceProperty();
        //await insuranceService.CreateCompanyLiability();
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
        // Adam Å was here again :D
        //
        // emily was here

        // Adam Å was here again :D
        //david was not here too
        //Kasper was here again 

        //Console.WriteLine(RiskzoneLevel.Zone1);

    }

}
