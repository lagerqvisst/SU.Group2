using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using SU.Backend.Models.Employee;
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
        public DbSet<EmployeeRoleAssignment> EmployeeRoleAssignments { get; set; }

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

            // Ställ in konverteraren för EmployeeRoleAssignment entiteten
            modelBuilder.Entity<EmployeeRoleAssignment>()
                .Property(er => er.Role)
                .HasConversion(employeeTypeConverter);

            modelBuilder.Entity<EmployeeRoleAssignment>()
                   .HasKey(er => er.EmployeeRoleAssignmentId);

            modelBuilder.Entity<EmployeeRoleAssignment>()
                .HasOne<Employee>(er => er.Employee) // Navigeringsegenskap
                .WithMany(e => e.RoleAssignments)   // Navigeringsegenskap i Employee, En Employee kan ha flera EmployeeRoleAssignments
                .HasForeignKey(er => er.EmployeeId); // FK
        }
    }
}
