using Microsoft.Extensions.DependencyInjection;
using SU.Backend.Database;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;
using SU.Backend.Controllers;
using Microsoft.Extensions.Logging;

namespace SU.Backend.Configuration
{
    public static class ConfigBackend
    {
        public static void AddBackendServices(this IServiceCollection services)
        {
            // Registrera tjänster och deras beroenden här

            //DB Service 
            services.AddDbContext<Context>();
            services.AddScoped<UnitOfWork>();


            //Test Service
            services.AddScoped<IDatabaseTestService, DatabaseTestServices>();

            //API Service
            services.AddHttpClient<IRandomGenerationService, RandomGenerationService>();

            //Controllers
            services.AddTransient<EmployeeController>();
            services.AddTransient<PrivateCustomerController>(); 
            services.AddTransient<LoginController>();
            services.AddTransient<CompanyCustomerController>();

            // Lägg till fler tjänster här
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPrivateCustomerService, PrivateCustomerService>();
            services.AddScoped<ICompanyCustomerService, CompanyCustomerService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProspectService, ProspectService>();
            services.AddScoped<ICommissionService, CommissionService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            // Loggning
            services.AddLogging(configure => configure.AddConsole());

        }
    }
}
