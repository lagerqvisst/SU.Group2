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
                        services.AddBackendServices(context.Configuration); 
                    })
                    .Build();




    }

}
