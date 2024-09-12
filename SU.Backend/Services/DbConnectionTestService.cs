using Microsoft.Extensions.Logging;
using SU.Backend.Database;
using SU.Backend.Services.Interfaces;
using System.Threading.Tasks;

namespace SU.Backend.Services
{
    public class DbConnectionTestService : IDbConnectionTestService
    {
        private readonly ILogger<DbConnectionTestService> _logger;
        private readonly DbConnection _dbConnection;

        public DbConnectionTestService(ILogger<DbConnectionTestService> logger, DbConnection dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        public async Task<(bool Success, string Message)> TestDbConnection()
        {
            _logger.LogInformation("Testing database connection");

            try
            {
                if (await _dbConnection.Database.CanConnectAsync())
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
