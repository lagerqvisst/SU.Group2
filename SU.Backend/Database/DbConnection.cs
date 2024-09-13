using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SU.Backend.Database
{
    public class DbConnection : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbConnection(DbContextOptions<DbConnection> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Hämta anslutningssträng från appsettings.json, exponerar inte känslig info här.
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
