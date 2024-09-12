using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SU.Backend.Configuration; // Lägg till detta för att använda DIConfiguration
using SU.Backend.Services.Interfaces;
using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Använd DI-konfigurationsmetoden för att registrera tjänster
                services.AddApplicationServices();

                // Lägg till en logger
                services.AddLogging(configure => configure.AddConsole());
            })
            .Build();

        // Hämta tjänsten och använd den
        var dbConnectionTestService = host.Services.GetRequiredService<IDbConnectionTestService>();
        var result = dbConnectionTestService.TestDbConnection();

        // Logga resultatet till konsolen
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation(result.Result.Message);


    }
}
