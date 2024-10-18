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
            // Registers all services and controllers for the backend

            //DB Service 
            services.AddDbContext<Context>();
            services.AddScoped<UnitOfWork>();

            //API Service (only used for testing)
            services.AddHttpClient<IRandomGenerationService, RandomGenerationService>();

            //Controllers
            services.AddTransient<EmployeeController>();
            services.AddTransient<PrivateCustomerController>(); 
            services.AddTransient<LoginController>();
            services.AddTransient<CompanyCustomerController>();
            services.AddTransient<InsuranceController>();
            services.AddTransient<PrivateCoverageController>();
            services.AddTransient<ProspectController>();
            services.AddTransient<StatisticsController>();
            services.AddTransient<ComissionController>();
            services.AddTransient<InvoiceController>();

            // Services 
            services.AddScoped<IDatabaseTestService, DatabaseTestServices>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPrivateCustomerService, PrivateCustomerService>();
            services.AddScoped<ICompanyCustomerService, CompanyCustomerService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProspectService, ProspectService>();
            services.AddScoped<ICommissionService, CommissionService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IPrivateCoverageService, PrivateCoverageService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IDataExportService, DataExportService>();

            // Loggning
            services.AddLogging(configure => configure.AddConsole());

        }
    }
}
