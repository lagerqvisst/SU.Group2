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
    /// <summary>
    /// This class is responsible for handling the database context for the application.
    /// Entity Framework Core is used to handle the database operations.
    /// OnModelCreating is used to define relationships between entities.
    /// </summary>
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
        public DbSet<CompanyCustomer> CompanyCustomers { get; set; }
        public DbSet<RiskZone> Riskzones { get; set; }
        public DbSet<VehicleInsuranceOption> VehicleInsuranceOptions { get; set; }
        public DbSet<VehicleInsuranceCoverage> VehicleInsuranceCoverages { get; set; }
        public DbSet<LiabilityCoverageOption> LiabilityCoverageOption { get; set; }
        public DbSet<LiabilityCoverage> LiabilityCoverages { get; set; }
        public DbSet<PropertyAndInventoryCoverage> PropertyAndInventoryCoverages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // We created a utility class to improve readability.
            // See Utility folder for more information.
            modelBuilder.ConfigureEnumsAsStrings();
            modelBuilder.ConfigureDecimals();
            modelBuilder.SeedRiskzones();
            modelBuilder.SeedPrivateCoverageOptions();
            modelBuilder.SeedIsuranceAddonTypes();
            modelBuilder.SeedVehicleCoverageOptions();
            modelBuilder.SeedLiabilityCoverageOptions();
            modelBuilder.SeedEmployees();
            modelBuilder.SeedEmployeeRoleAssignments();


            // This is where we define the relationships between the entities.
            #region Insurance

            //Configure the relationship between Insurance and InsurancePolicyHolder
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.InsurancePolicyHolder)  // An Insurance has an InsurancePolicyHolder
                .WithOne(iph => iph.Insurance)         // An InsurancePolicyHolder has one Insurance
                .HasForeignKey<InsurancePolicyHolder>(iph => iph.InsuranceId)  // FK in InsurancePolicyHolder
                .OnDelete(DeleteBehavior.Cascade);     // Delete the InsurancePolicyHolder if the Insurance is deleted

            //Configure the relationship between Insurance and Seller
            modelBuilder.Entity<Insurance>()
               .HasOne(e => e.Seller) // An Insurance has a Seller
               .WithMany(i => i.Insurances) // A Seller can have many Insurances (can sell many Insurances)
               .HasForeignKey(e => e.SellerId) // FK in Insurance
               .OnDelete(DeleteBehavior.Cascade); // Delete the Seller if the Insurance is deleted

            //Configure the relationship between Insurance and InsuranceAddons
            modelBuilder.Entity<Insurance>()
               .HasMany(pi => pi.InsuranceAddons) // An Insurance can have many InsuranceAddons (not limited to any type of insurnace)
               .WithOne(ia => ia.Insurance) // Each InsuranceAddon has one Insurance
               .HasForeignKey(ia => ia.InsuranceId) // FK in InsuranceAddon
               .OnDelete(DeleteBehavior.Cascade); // Delete the InsuranceAddon if the Insurance is deleted

            //Configure the relationship between Insurance and InsuranceCoverage
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.InsuranceCoverage) // An Insurance has an InsuranceCoverage
                .WithOne(ic => ic.Insurance) // An InsuranceCoverage has one Insurance (this is a bridge to the differnt types of insurances)
                .HasForeignKey<InsuranceCoverage>(ic => ic.InsuranceId) // FK in InsuranceCoverage
                .OnDelete(DeleteBehavior.Cascade); // Delete the InsuranceCoverage if the Insurance is deleted

            #endregion


            #region Employee

            //Configure the relationship between Employee and an Employee (manager/employee relationship (recursive))
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager) // An Employee has a Manager
                .WithMany() // A Manager can have many Employees
                .HasForeignKey(e => e.ManagerId) // FK in Employee
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletion of Manager. If a Manager is deleted, the Employee will still exist.

            #endregion

            #region Private Customer

            //Configure the relationship between PrivateCustomer and InsurancePolicyHolder
            modelBuilder.Entity<PrivateCustomer>()
            .HasMany(pc => pc.InsurancePolicyHolders) // A PrivateCustomer can have many InsurancePolicyHolders
            .WithOne(iph => iph.PrivateCustomer) // Each InsurancePolicyHolder has one PrivateCustomer
            .HasForeignKey(iph => iph.PrivateCustomerId) // Foreign key in InsurancePolicyHolder
            .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of PrivateCustomer if there are related InsurancePolicyHolders

            #endregion

            #region Company Customer

            //Configure the relationship between CompanyCustomer and InsurancePolicyHolder
            modelBuilder.Entity<CompanyCustomer>()
                .HasMany(cc => cc.InsurancePolicyHolders) // A CompanyCustomer can have many InsurancePolicyHolders
                .WithOne(iph => iph.CompanyCustomer) // Each InsurancePolicyHolder has one CompanyCustomer
                .HasForeignKey(iph => iph.CompanyCustomerId) // Foreign key in InsurancePolicyHolder
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of CompanyCustomer if there are related InsurancePolicyHolders
            #endregion

            #region Insurance Addon

            //Configure the relationship between InsuranceAddon and InsuranceAddonType
            modelBuilder.Entity<InsuranceAddon>()
                .HasOne(ia => ia.InsuranceAddonType) // An InsuranceAddon has one InsuranceAddonType
                .WithMany() // An InsuranceAddonType can have many InsuranceAddons
                .HasForeignKey(ia => ia.InsuranceAddonTypeId) // FK in InsuranceAddon
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of InsuranceAddonType

            #endregion

            #region Insurance Coverage
            //Configure the relationship between InsuranceCoverage and Insurance
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.Insurance) // An InsuranceCoverage has an Insurance
                .WithOne(i => i.InsuranceCoverage) // An Insurance has one InsuranceCoverage
                .HasForeignKey<InsuranceCoverage>(ic => ic.InsuranceId) // FK in InsuranceCoverage
                .OnDelete(DeleteBehavior.Cascade); // Delete the InsuranceCoverage if the Insurance is deleted

            //Configure the relationship between InsuranceCoverage and InsuranceCoverage
            modelBuilder.Entity<InsuranceCoverage>() // An InsuranceCoverage can have one of each type of coverage
                .HasOne(ic => ic.LiabilityCoverage) // An InsuranceCoverage has a LiabilityCoverage
                .WithOne(lc => lc.InsuranceCoverage) // A LiabilityCoverage has one InsuranceCoverage
                .HasForeignKey<LiabilityCoverage>(lc => lc.InsuranceCoverageId) // FK in LiabilityCoverage
                .OnDelete(DeleteBehavior.Cascade); // Delete the LiabilityCoverage if the InsuranceCoverage is deleted

            //Configure the relationship between InsuranceCoverage and InsuranceCoverage
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.PrivateCoverage) // An InsuranceCoverage has a PrivateCoverage
                .WithOne(pc => pc.InsuranceCoverage) // A PrivateCoverage has one InsuranceCoverage
                .HasForeignKey<PrivateCoverage>(pc => pc.InsuranceCoverageId) // FK in PrivateCoverage
                .OnDelete(DeleteBehavior.Cascade); // Delete the PrivateCoverage if the InsuranceCoverage is deleted

            //Configure the relationship between InsuranceCoverage and InsuranceCoverage
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.PropertyAndInventoryCoverage) // An InsuranceCoverage has a PropertyAndInventoryCoverage
                .WithOne(pic => pic.InsuranceCoverage) // A PropertyAndInventoryCoverage has one InsuranceCoverage
                .HasForeignKey<PropertyAndInventoryCoverage>(pic => pic.InsuranceCoverageId) // FK in PropertyAndInventoryCoverage
                .OnDelete(DeleteBehavior.Cascade); // Delete the PropertyAndInventoryCoverage if the InsuranceCoverage is deleted

            //Configure the relationship between InsuranceCoverage and InsuranceCoverage
            modelBuilder.Entity<InsuranceCoverage>()
                .HasOne(ic => ic.VehicleInsuranceCoverage) // An InsuranceCoverage has a VehicleInsuranceCoverage
                .WithOne(vic => vic.InsuranceCoverage) // A VehicleInsuranceCoverage has one InsuranceCoverage
                .HasForeignKey<VehicleInsuranceCoverage>(vic => vic.InsuranceCoverageId) // FK in VehicleInsuranceCoverage
                .OnDelete(DeleteBehavior.Cascade); // Delete the VehicleInsuranceCoverage if the InsuranceCoverage is deleted

            #endregion

            #region Private Coverage
            // Configure PrivateCoverage -> PrivateCoverageOption relationship
            modelBuilder.Entity<PrivateCoverage>()
                .HasOne(pc => pc.PrivateCoverageOption) // A PrivateCoverage has a PrivateCoverageOption
                .WithMany(pco => pco.PrivateCoverages)  // A PrivateCoverageOption can have many PrivateCoverages
                .HasForeignKey(pc => pc.PrivateCoverageOptionId) // FK in PrivateCoverage
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of PrivateCoverageOption


            #endregion

            //Created due to some relational issues. 
            modelBuilder.Entity<RiskZone>()
            .HasKey(x => x.RiskZoneId);

            //Created due to some relational issues
            modelBuilder.Entity<VehicleInsuranceOption>()
                .HasKey(x => x.VehicleInsuranceOptionId);
            #region Vehicle Coverage

            // Configure VehicleInsuranceCoverage -> Riskzone relationship
            modelBuilder.Entity<VehicleInsuranceCoverage>()
                .HasOne(vic => vic.RiskZone) // Navigation to Riskzone
                .WithMany(rz => rz.VehicleInsuranceCoverages) // Riskzone has many VehicleInsuranceCoverages
                .HasForeignKey(vic => vic.RiskzoneId) // FK on VehicleInsuranceCoverage
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Riskzone

            // Configure VehicleInsuranceCoverage -> VehicleInsuranceOption relationship
            modelBuilder.Entity<VehicleInsuranceCoverage>()
                .HasOne(vic => vic.VehicleInsuranceOption) // Navigation to VehicleInsuranceOption
                .WithMany(vo => vo.VehicleInsuranceCoverages) // VehicleInsuranceOption has many VehicleInsuranceCoverages
                .HasForeignKey(vic => vic.VehicleInsuranceOptionId) // FK on VehicleInsuranceCoverage
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of VehicleInsuranceOption

            #endregion

            #region LiabilityOption
            // Configure LiabilityCoverage -> LiabilityCoverageOption relationship
            modelBuilder.Entity<LiabilityCoverage>()
                .HasOne(lc => lc.LiabilityCoverageOption) // Navigation to LiabilityCoverageOption
                .WithMany(l => l.LiabilityCoverages) // LiabilityCoverageOption has many LiabilityCoverages
                .HasForeignKey(lc => lc.LiabilityCoverageOptionId) // FK on LiabilityCoverage
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of LiabilityCoverageOption
            #endregion

           


        }
    }
}
