using Microsoft.Extensions.DependencyInjection;
using SU.Backend.Database;
using SU.Backend.Services;
using SU.Backend.Services.Interfaces;

namespace SU.Backend.Configuration
{
    public static class DIConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Registrera tjänster och deras beroenden här
            services.AddDbContext<DbConnection>();
            services.AddScoped<IDbConnectionTestService, DbConnectionTestService>();

            // Lägg till fler tjänster här
        }
    }
}
