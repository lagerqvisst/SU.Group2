using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using SU.Backend.Models;
using SU.Backend.Models.Enums;

namespace SU.Backend.Database
{
    public class DbConnection : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbConnection(DbContextOptions<DbConnection> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        // DbSets
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Hämta anslutningssträng från appsettings.json
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // För att enumen EmployeeType ska sparas som en sträng i databasen
            var employeeTypeConverter = new ValueConverter<EmployeeType, string>(
                v => v.ToString(), // Konvertera enum till sträng när du sparar till databasen
                v => (EmployeeType)Enum.Parse(typeof(EmployeeType), v) // Konvertera sträng till enum när du läser från databasen
            );

            modelBuilder.Entity<Employee>()
                .Property(e => e.Role)
                .HasConversion(employeeTypeConverter);
        }
    }
}
