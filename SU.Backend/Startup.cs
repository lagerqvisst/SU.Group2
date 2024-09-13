using Microsoft.Extensions.DependencyInjection;
using SU.Backend.Database;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;
using SU.Backend.Controllers;
using Microsoft.Extensions.Logging;

namespace SU.Backend.Configuration
{
    public static class DIConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Registrera tjänster och deras beroenden här

            //DB Service 
            services.AddDbContext<DbConnection>();

            //Test Service
            services.AddScoped<IDbConnectionTestService, DbConnectionTestService>();

            //API Service
            services.AddHttpClient<IRandomGenerationService, RandomGenerationService>();

            //Controllers
            services.AddTransient<EmployeeController>();

            // Lägg till fler tjänster här
            services.AddScoped<IEmployeeService, EmployeeService>();

            // Loggning
            services.AddLogging(configure => configure.AddConsole());

        }
    }
}
