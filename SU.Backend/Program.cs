using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU.Backend.Configuration;
using SU.Backend.Controllers;
using SU.Backend.Database;
using SU.Backend.Helper;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurances.Coverage;
using SU.Backend.Services.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
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
        ///Test InsuranceCreateService
        var InsuranceCreateService = host.Services.GetRequiredService<IInsuranceCreateService>();
        var InsuranceCreateController = host.Services.GetRequiredService<InsuranceCreateController>();
        var privateCoverageService = host.Services.GetRequiredService<IPrivateCoverageService>();

        #endregion

        #region Prospect tests
        var prospectService = host.Services.GetRequiredService<IProspectService>();
        #endregion

        #region Statistics tests
        var commissionService = host.Services.GetRequiredService<ICommissionService>();

        #endregion

        //Export testing
        
        /*
        var dataexportservice = host.Services.GetRequiredService<IDataExportService>();
        var comissionService = host.Services.GetRequiredService<ICommissionService>();
        var invoiceService = host.Services.GetRequiredService<IInvoiceService>();

        var invoices = await invoiceService.GenerateInvoiceData();

        await dataexportservice.ExportInvoicesToExcel(invoices.InvoiceData);*/


    }

}
