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
            services.AddScoped<UnitOfWork>();


            //Test Service
            services.AddScoped<IDbConnectionTestService, DbConnectionTestService>();

            //API Service
            services.AddHttpClient<IRandomGenerationService, RandomGenerationService>();

            //Controllers
            services.AddTransient<EmployeeController>();
            services.AddTransient<PrivateCustomerController>(); 

            // Lägg till fler tjänster här
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPrivateCustomerService, PrivateCustomerService>();

            // Loggning
            services.AddLogging(configure => configure.AddConsole());

        }
    }
}
