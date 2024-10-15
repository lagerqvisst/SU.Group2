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
        public DbSet<InsuranceCoverage> InsuranceCoverages { get; set; }
        public DbSet<InsuranceAddon> InsuranceAddons { get; set; }
        public DbSet<InsuranceAddonType> InsuranceAddonTypes { get; set; }
        public DbSet<PrivateCoverageOption> PrivateCoverageOption { get; set; }
        public DbSet<PrivateCoverage> PrivateCoverages { get; set; }
        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
        public DbSet<Riskzone> Riskzones { get; set; }
        public DbSet<VehicleInsuranceOption> VehicleInsuranceOptions { get; set; }

        public DbSet<LiabilityCoverageOption> LiabilityCoverageOption { get; set; }


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
            modelBuilder.SeedVehicleCoverageOptions();
            modelBuilder.SeedLiabilityCoverageOptions();

            // Definiera relationer
            #region Insurance
            /*
            modelBuilder.Entity<InsurancePolicyHolder>()
                .HasOne(i => i.Insurance)
                .WithOne(i => i.InsurancePolicyHolder)
                .HasForeignKey<Insurance>(ic => ic.InsurancePolicyHolderId)
                .OnDelete(DeleteBehavior.Cascade); */

            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.InsurancePolicyHolder)  // En Insurance har en InsurancePolicyHolder
                .WithOne(iph => iph.Insurance)         // En InsurancePolicyHolder har en Insurance
                .HasForeignKey<InsurancePolicyHolder>(iph => iph.InsuranceId)  // FK i InsurancePolicyHolder
                .OnDelete(DeleteBehavior.Cascade);     // Radera InsurancePolicyHolder när Insurance tas bort

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
            .OnDelete(DeleteBehavior.Restrict); // Förhindra kaskadradering vid borttagning av PrivateCustomer

            #endregion

            #region Company Customer
            modelBuilder.Entity<CompanyCustomer>()
                .HasMany(cc => cc.InsurancePolicyHolders)
                .WithOne(iph => iph.CompanyCustomer)
                .HasForeignKey(iph => iph.CompanyCustomerId)
                .OnDelete(DeleteBehavior.Restrict);
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


            #endregion

            modelBuilder.Entity<Riskzone>()
            .HasKey(x => x.RiskzoneId);

            modelBuilder.Entity<VehicleInsuranceOption>()
                .HasKey(x => x.VehicleInsuranceOptionId);
            #region Vehicle Coverage

            // Configure VehicleInsuranceCoverage -> Riskzone relationship
            modelBuilder.Entity<VehicleInsuranceCoverage>()
                .HasOne(vic => vic.Riskzone) // Navigation to Riskzone
                .WithMany(rz => rz.VehicleInsuranceCoverages) // Riskzone has many VehicleInsuranceCoverages
                .HasForeignKey(vic => vic.RiskzoneId) // FK on VehicleInsuranceCoverage
                .OnDelete(DeleteBehavior.Restrict);

            // Configure VehicleInsuranceCoverage -> VehicleInsuranceOption relationship
            modelBuilder.Entity<VehicleInsuranceCoverage>()
                .HasOne(vic => vic.VehicleInsuranceOption) // Navigation to VehicleInsuranceOption
                .WithMany(vo => vo.VehicleInsuranceCoverages) // VehicleInsuranceOption has many VehicleInsuranceCoverages
                .HasForeignKey(vic => vic.VehicleInsuranceOptionId) // FK on VehicleInsuranceCoverage
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

#region LiabilityOption
            modelBuilder.Entity<LiabilityCoverage>()
                .HasOne(lc => lc.LiabilityCoverageOption)
                .WithMany(l => l.LiabilityCoverages)
                .HasForeignKey(lc => lc.LiabilityCoverageOptionId)
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
