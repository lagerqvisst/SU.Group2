using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employee;
using SU.Backend.Models.Insurance;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Insurance.Coverage;
using System.ComponentModel;
using SU.Backend.Database.Utility;

namespace SU.Backend.Database
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        // DbSets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRoleAssignment> EmployeeRoleAssignments { get; set; }
        public DbSet<PrivateCustomer> PrivateCustomers { get; set; }
        public DbSet<InsurancePolicyHolder> InsurancePolicyHolders { get; set; }
        public DbSet<Insurance> Insurances { get; set; } // Changed to plural
        public DbSet<InsuredPerson> InsuredPersons { get; set; }
        public DbSet<InsuranceCoverage> InsuranceCoverages { get; set; }
        public DbSet<InsuranceAddon> InsuranceAddons { get; set; }
        public DbSet<InsuranceAddonType> InsuranceAddonTypes { get; set; }
        public DbSet<PrivateCoverageOption> PrivateCoverageOption { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Använda den generiska konverteraren för enums & decimalvärden.
            modelBuilder.ConfigureEnumsAsStrings();
            modelBuilder.ConfigureDecimals();
            modelBuilder.SeedRiskZones();
            modelBuilder.SeedPrivateCoverageOptions();

            // Definiera relationer
            modelBuilder.Entity<Insurance>()
                .HasOne(pi => pi.InsurancePolicyHolder)
                .WithMany()
                .HasForeignKey(pi => pi.InsurancePolicyHolderId)
                .OnDelete(DeleteBehavior.Restrict);

            //En försäkringstagare kan vara antingen privatperson eller företag.
            modelBuilder.Entity<InsurancePolicyHolder>()
                .HasOne(ip => ip.PrivateCustomer)
                .WithMany(pc => pc.InsurancePolicyHolders)
                .HasForeignKey(ip => ip.PrivateCustomerId);

            modelBuilder.Entity<InsurancePolicyHolder>()
                .HasOne(ip => ip.CompanyCustomer)
                .WithMany(cc => cc.InsurancePolicyHolders)
                .HasForeignKey(ip => ip.CompanyCustomerId);

            //En privat person kan inneha flera försäkringar (dvs. vara många försäkringstagare)
            modelBuilder.Entity<PrivateCustomer>()
                .HasMany(pc => pc.InsurancePolicyHolders)
                .WithOne(ip => ip.PrivateCustomer)
                .HasForeignKey(ip => ip.PrivateCustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            //Samma som privatperson, en företagskund kan ha flera försäkringar (dvs. vara många försäkringstagare)
            modelBuilder.Entity<CompanyCustomer>()
                .HasMany(cc => cc.InsurancePolicyHolders)
                .WithOne(ip => ip.CompanyCustomer)
                .HasForeignKey(ip => ip.CompanyCustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            //En försäkring har en IsuranceCoverage (den agerar grundklass för de olika försäkringstyperna)
            //Förutom privatförsäkringar har de olika försäkringstyperna flera olika attribut.

            modelBuilder.Entity<Insurance>()
                .HasOne(pi => pi.InsuranceCoverage) // En Insurance har en InsuranceCoverage
                .WithOne() // En InsuranceCoverage är kopplad till en Insurance
                .HasForeignKey<Insurance>(pi => pi.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Restrict);


            //En försäkring kan ha tillägg, just nu enbart relevant för privatförsäkringar enligt bilagan.
            //Men implementationen stödjer fler tillägg för framtiden. 
            modelBuilder.Entity<Insurance>()
                .HasMany(pi => pi.InsuranceAddons)
                .WithOne(ia => ia.Insurance)
                .HasForeignKey(ia => ia.PrivateInsuranceId)
                .OnDelete(DeleteBehavior.Cascade);

            //Ett tillägg har olika typer, de olika typerna har olika uträkningar.
            //Delade upp så det inte blir overcroweden med nullvärden i databasen. 
            modelBuilder.Entity<InsuranceAddon>()
                .HasOne(ia => ia.InsuranceAddonType) // Definiera relationen
                .WithMany() // Anta att det inte finns en navigationsproperty på InsuranceAddonType
                .HasForeignKey(ia => ia.InsuranceAddonTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            //Insurance Coverage. Har frivilligt deltagande till de olika försäkringstyperna.
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.LiabilityCoverage)
                .WithOne(lc => lc.InsuranceCoverage)
                .HasForeignKey<LiabilityCoverage>(lc => lc.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.PrivateCoverage)
                .WithOne(pc => pc.InsuranceCoverage)
                .HasForeignKey<PrivateCoverage>(pc => pc.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.PropertyAndInventoryCoverage)
                .WithOne(pic => pic.InsuranceCoverage)
                .HasForeignKey<PropertyAndInventoryCoverage>(pic => pic.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.VehicleInsuranceCoverage)
                .WithOne(vic => vic.InsuranceCoverage)
                .HasForeignKey<VehicleInsuranceCoverage>(vic => vic.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PrivateCoverage>()
                .HasOne(pc => pc.PrivateCoverageOption) // En PrivateCoverage har en PrivateCoverageOption
                .WithOne() // En PrivateCoverageOption kan existera utan att vara kopplad
                .HasForeignKey<PrivateCoverage>(pco => pco.PrivateCoverageOptionId)
                .OnDelete(DeleteBehavior.Restrict); // Ta bort om den inte ska radera relaterade objekt

            modelBuilder.Entity<VehicleInsuranceCoverage>()
                .HasOne(vic => vic.RiskZone) // En VehicleInsuranceCoverage har en RiskZone
                .WithOne() // En RiskZone är kopplad till en VehicleInsuranceCoverage
                .HasForeignKey<VehicleInsuranceCoverage>(vic => vic.RiskZoneId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PrivateCoverageOption>()
                .HasKey(pco => pco.PrivateCoverageOptionId); // Definiera primärnyckeln

            // Definiera relationer för VehicleInsuranceCoverage och RiskZone
            // Gjorde en egen tabell för Riskzone då en enum inte tillåter decimaler. 


            modelBuilder.Entity<RizkZone>()
                .HasKey(rz => rz.RiskZoneId); // Definiera primärnyckeln
        }
    }
}
