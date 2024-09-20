using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Database.Repositories;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance;
using SU.Backend.Models.Insurance.Coverage;
using SU.Backend.Services.Interfaces;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class DatabaseTestServices : IDatabaseTestService
    {
        private readonly ILogger<DatabaseTestServices> _logger;
        private readonly Context _context;



        public DatabaseTestServices(ILogger<DatabaseTestServices> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<(bool Success, string Message)> RecreateDb()
        {
            _logger.LogInformation("Recreating database");

            try
            {
                var created = await _context.Database.EnsureCreatedAsync();
                return (created, "Database recreated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while recreating the database");
                return (false, "An error occurred while recreating the database");
            }
        }

        public async Task<(bool Success, string Message)> TestDbConnection()
        {
            _logger.LogInformation("Testing database connection");

            try
            {
                if (await _context.Database.CanConnectAsync())
                {
                    _logger.LogInformation("Database connection successful");
                    return (true, "Database connection successful");
                }
                else
                {
                    _logger.LogWarning("Database connection failed");
                    return (false, "Database connection failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while testing the database connection");
                return (false, "An error occurred while testing the database connection");
            }
        }

        
    }
}

