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

        var employeeController = host.Services.GetRequiredService<EmployeeController>();
        await employeeController.CreateRandomNewEmployee(EmployeeType.OutsideSales);
    }

}
