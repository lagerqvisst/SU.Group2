using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SU.Backend.Models.Customers;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Insurances.Coverage;
using System.ComponentModel;
using SU.Backend.Database.Utility;
using SU.Backend.Models.Insurances.Prospects;

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
        public DbSet<PrivateCoverage> PrivateCoverages { get; set; }
        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }





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
            modelBuilder.SeedRiskzones();
            modelBuilder.SeedPrivateCoverageOptions();
            modelBuilder.SeedIsuranceAddonTypes();

            // Definiera relationer
            #region Insurance 
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.InsurancePolicyHolder) // En Insurance har en InsurancePolicyHolder
                .WithMany(iph => iph.Insurances)      // En InsurancePolicyHolder har många Insurance
                .HasForeignKey(i => i.InsurancePolicyHolderId) // FK
                .OnDelete(DeleteBehavior.Cascade);    // Radera relaterade Insurance vid borttagning av InsurancePolicyHolder

            modelBuilder.Entity<Insurance>()
               .HasOne(e => e.Seller)
               .WithMany(i => i.Insurances)
               .HasForeignKey(e => e.SellerId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Insurance>()
               .HasMany(pi => pi.InsuranceAddons)
               .WithOne(ia => ia.Insurance)
               .HasForeignKey(ia => ia.InsuranceId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.InsuranceCoverage)
                .WithOne(ic => ic.Insurance) // Assuming InsuranceCoverage has a reference back to Insurance
                .HasForeignKey<InsuranceCoverage>(ic => ic.InsuranceId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region InsurancePolicyHolder
            /*
            modelBuilder.Entity<InsurancePolicyHolder>()
                .HasOne(iph => iph.Insurance)
                .WithOne(i => i.InsurancePolicyHolder)
                .HasForeignKey<InsurancePolicyHolder>(iph => iph.InsuranceId)
                .OnDelete(DeleteBehavior.Restrict); // Använd Restrict */
            #endregion


            #region Employee

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany()
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Private Customer

            modelBuilder.Entity<PrivateCustomer>()
                .HasMany(pc => pc.InsurancePolicyHolders) // A PrivateCustomer can have many InsurancePolicyHolders
                .WithOne(iph => iph.PrivateCustomer) // Each InsurancePolicyHolder has one PrivateCustomer
                .HasForeignKey(iph => iph.PrivateCustomerId) // Foreign key in InsurancePolicyHolder
                .OnDelete(DeleteBehavior.Cascade); // Optional: specify delete behavior */
            #endregion

            #region Company Customer
            modelBuilder.Entity<CompanyCustomer>()
                .HasMany(cc => cc.InsurancePolicyHolders)
                .WithOne(iph => iph.CompanyCustomer)
                .HasForeignKey(iph => iph.CompanyCustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Insurance Addon

            //Ett tillägg har olika typer, de olika typerna har olika uträkningar.
            //Delade upp så det inte blir overcroweden med nullvärden i databasen. 
            modelBuilder.Entity<InsuranceAddon>()
                .HasOne(ia => ia.InsuranceAddonType) // Definiera relationen
                .WithMany() // Anta att det inte finns en navigationsproperty på InsuranceAddonType
                .HasForeignKey(ia => ia.InsuranceAddonTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Insurance Coverage
            // En försäkring har en InsuranceCoverage
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.Insurance)
                .WithOne(i => i.InsuranceCoverage)
                .HasForeignKey<InsuranceCoverage>(ic => ic.InsuranceId)
                .OnDelete(DeleteBehavior.Cascade);

            //Insurance Coverage. Har frivilligt deltagande till de olika försäkringstyperna.
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.LiabilityCoverage)
                .WithOne(lc => lc.InsuranceCoverage)
                .HasForeignKey<LiabilityCoverage>(lc => lc.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.PrivateCoverage)
                .WithOne(pc => pc.InsuranceCoverage)
                .HasForeignKey<PrivateCoverage>(pc => pc.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.PropertyAndInventoryCoverage)
                .WithOne(pic => pic.InsuranceCoverage)
                .HasForeignKey<PropertyAndInventoryCoverage>(pic => pic.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.VehicleInsuranceCoverage)
                .WithOne(vic => vic.InsuranceCoverage)
                .HasForeignKey<VehicleInsuranceCoverage>(vic => vic.InsuranceCoverageId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Private Coverage
            modelBuilder.Entity<PrivateCoverage>()
                .HasOne(pc => pc.PrivateCoverageOption) // En PrivateCoverage har en PrivateCoverageOption
                .WithMany(pco => pco.PrivateCoverages)  // En PrivateCoverageOption kan ha många PrivateCoverages
                .HasForeignKey(pc => pc.PrivateCoverageOptionId)
                .OnDelete(DeleteBehavior.Restrict);     // Statisk data ska inte tas bort

            modelBuilder.Entity<PrivateCoverage>()
                .HasOne(pc => pc.InsuredPerson)
                .WithMany(ip => ip.PrivateCoverages) // Ändra till WithMany
                .HasForeignKey(pc => pc.InsuredPersonId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Vehicle Coverage
            modelBuilder.Entity<VehicleInsuranceCoverage>()
                .HasOne(vic => vic.Riskzone) // En VehicleInsuranceCoverage har en Riskzone
                .WithMany() // En Riskzone är kopplad till en VehicleInsuranceCoverage
                .HasForeignKey(vic => vic.RiskzoneId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Prospect
            modelBuilder.Entity<Prospect>()
                .HasOne(p => p.PrivateCustomer)
                .WithMany()
                .HasForeignKey(p => p.PrivateCustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prospect>()
                .HasOne(p => p.CompanyCustomer)
                .WithMany()
                .HasForeignKey(p => p.CompanyCustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            //En säljare kan bli assignad flera prospects
            modelBuilder.Entity<Prospect>()
                .HasOne(p => p.Seller)
                .WithMany()
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion


        }
    }
}
