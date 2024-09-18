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
                services.AddApplicationServices();
            })
            .Build();


        ///Test EmplyeeController
        var employeeController = host.Services.GetRequiredService<EmployeeController>();
        await employeeController.CreateRandomNewEmployee(EmployeeType.InsideSales);

        ///Test PrivateCustomerController
        //var privateCustomerController = host.Services.GetRequiredService<PrivateCustomerController>();
        //await privateCustomerController.GenerateRandomPrivateCustomer();

        ///Test LoginController
        //var loginController = host.Services.GetRequiredService<LoginController>();
        //await loginController.Authentication("cene", "zigzag");

    }

}
