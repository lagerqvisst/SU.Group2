using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SU.Backend.Helper;
using SU.Backend.Models.Employees;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Enums.Insurance.Addons;
using SU.Backend.Models.Insurances;
using SU.Backend.Models.Insurances.Coverage;
using System;

namespace SU.Backend.Database.Utility
{
    /// <summary>
    /// This class contains extension methods for the ModelBuilder class.
    /// They are called upon in the context when we are configuring the database.
    /// To limit the amount of code and increase readability in the context class we use this static class.
    /// </summary>
    /// 

    // This class checks for all entities that contains enums.
    // If the entity contains an enum, it will convert the enum to a string.
    // This is done to make the database more readable.
    // Instead of the database column showing the enum value, it will show the string value.
    public static class ModelBuilderExtensions
    {
        public static void ConfigureEnumsAsStrings(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.IsEnum)
                    {
                        var enumType = property.ClrType;
                        var converter = (ValueConverter)Activator.CreateInstance(
                            typeof(EnumToStringConverter<>).MakeGenericType(enumType)
                        );

                        property.SetValueConverter(converter);
                    }
                }
            }
        }

        // This class checks for all entities that contains decimal values.
        // It is needed because the default value for decimal is decimal(18,2).
        // This needs to be explicitly set for each decimal value in the database.
        // To reduce amount of code in context class and increase readability, we use this class.
        public static void ConfigureDecimals(this ModelBuilder modelBuilder)
        {
            var decimalTypes = new[]
            {
                (typeof(InsuranceAddonType), new[] { "BaseExtraPremium", "CoverageAmount" }),
                (typeof(Insurance), new[] { "Premium"}),
                (typeof(PrivateCoverageOption), new[] { "MonthlyPremium", "CoverageAmount" }),
                (typeof(VehicleInsuranceCoverage), new[] { "MonthlyPremium", "CoverageAmount", }),
                (typeof(PropertyAndInventoryCoverage), new[] { "PropertyPremium", "PropertyValue", "InventoryPremium", "InventoryValue" }),
                (typeof(VehicleInsuranceOption), new[] { "Deductible", "OptionCost" }),
                (typeof(LiabilityCoverageOption), new[] { "MonthlyPremium" }),
            };

            foreach (var (type, properties) in decimalTypes)
            {
                foreach (var property in properties)
                {
                    modelBuilder.Entity(type)
                        .Property(property)
                        .HasColumnType("decimal(18,2)");
                }
            }
        }

        // This class seeds the database with the different risk zones.
        // The data is hardcoded and will be added to the database when the database is created.
        // The related data is found from the business doucmentation (Bilaga 2).
        public static void SeedRiskzones(this ModelBuilder modelBuilder)
        {
            // Seed för RizkZone
            modelBuilder.Entity<Riskzone>().HasData(
                new Riskzone { RiskzoneId = 1, RiskzoneLevel = RiskzoneLevel.Zone1, ZoneFactor = 1.3 },
                new Riskzone { RiskzoneId = 2, RiskzoneLevel = RiskzoneLevel.Zone2, ZoneFactor = 1.2 },
                new Riskzone { RiskzoneId = 3, RiskzoneLevel = RiskzoneLevel.Zone3, ZoneFactor = 1.1 },
                new Riskzone { RiskzoneId = 4, RiskzoneLevel = RiskzoneLevel.Zone4, ZoneFactor = 1.0 }
            );
        }

        // This class seeds the database with the different private coverage options.
        // The data is hardcoded and will be added to the database when the database is created.
        // The related data is found from the business doucmentation (Bilaga 2).
        public static void SeedPrivateCoverageOptions(this ModelBuilder modelBuilder)
        {
            //Child Accident and Health Insurance
            #region Seed for PrivateCoverageOption
            modelBuilder.Entity<PrivateCoverageOption>().HasData(
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 1,
                    CoverageAmount = 700_000m,
                    MonthlyPremium = 350m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },
            
            
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 2,
                    CoverageAmount = 900_000m,
                    MonthlyPremium = 450m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 3,
                    CoverageAmount = 1_100_000m,
                    MonthlyPremium = 550m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 4,
                    CoverageAmount = 1_300_000m,
                    MonthlyPremium = 650m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },

                // För 2024
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 5,
                    CoverageAmount = 750_000m,
                    MonthlyPremium = 375m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {

                    PrivateCoverageOptionId = 6,
                    CoverageAmount = 950_000m,
                    MonthlyPremium = 475m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 7,
                    CoverageAmount = 1_150_000m,
                    MonthlyPremium = 575m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 8,
                    CoverageAmount = 1_350_000m,
                    MonthlyPremium = 675m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.ChildAccidentAndHealthInsurance
                },

                //Adult Accident and Health Insurance
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 9,
                    CoverageAmount = 300_000m,
                    MonthlyPremium = 150m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.AdultAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 10,
                    CoverageAmount = 400_000m,
                    MonthlyPremium = 200m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.AdultAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 11,
                    CoverageAmount = 500_000m,
                    MonthlyPremium = 250m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.AdultAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 12, 
                    CoverageAmount = 350_000m,
                    MonthlyPremium = 175m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.AdultAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 13,
                    CoverageAmount = 450_000m,
                    MonthlyPremium = 225m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.AdultAccidentAndHealthInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 14,
                    CoverageAmount = 550_000m,
                    MonthlyPremium = 275m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.AdultAccidentAndHealthInsurance
                },

                //Adult Life Insurance

                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 15,
                    CoverageAmount = 300_000m,
                    MonthlyPremium = 150m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.AdultLifeInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 16,
                    CoverageAmount = 400_000m,
                    MonthlyPremium = 200m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.AdultLifeInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 17,
                    CoverageAmount = 500_000m,
                    MonthlyPremium = 250m,
                    StartDate = new DateTime(2023, 1, 1),
                    InsuranceType = InsuranceType.AdultLifeInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 18,
                    CoverageAmount = 350_000m,
                    MonthlyPremium = 175m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.AdultLifeInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 19,
                    CoverageAmount = 450_000m,
                    MonthlyPremium = 225m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.AdultLifeInsurance
                },
                new PrivateCoverageOption
                {
                    PrivateCoverageOptionId = 20,
                    CoverageAmount = 550_000m,
                    MonthlyPremium = 275m,
                    StartDate = new DateTime(2024, 1, 1),
                    InsuranceType = InsuranceType.AdultLifeInsurance
                }
            );
            #endregion
        }

        // This class seeds the database with the different insurance addon types.
        // The data is hardcoded and will be added to the database when the database is created.
        // The related data is found from the business doucmentation (Bilaga 2).
        public static void SeedIsuranceAddonTypes(this ModelBuilder modelBuilder)
        {
            //Seed for InsuranceAddonTypes

            #region Seed for SicknessAccident
            modelBuilder.Entity<InsuranceAddonType>().HasData(
                new InsuranceAddonType { InsuranceAddonTypeId = 1, CoverageAmount = 100_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 100_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 2, CoverageAmount = 200_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 200_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 3, CoverageAmount = 300_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 300_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 4, CoverageAmount = 400_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 400_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 5, CoverageAmount = 500_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 500_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 6, CoverageAmount = 600_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 600_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 7, CoverageAmount = 700_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 700_00m) },
                new InsuranceAddonType { InsuranceAddonTypeId = 8, CoverageAmount = 800_000m, Description = AddonType.SicknessAccident, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.SicknessAccident, 800_00m) },

                new InsuranceAddonType { InsuranceAddonTypeId = 9,  CoverageAmount = 500,  Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 500) },
                new InsuranceAddonType { InsuranceAddonTypeId = 10, CoverageAmount = 1000, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 1000) },
                new InsuranceAddonType { InsuranceAddonTypeId = 11, CoverageAmount = 1500, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 1500) },
                new InsuranceAddonType { InsuranceAddonTypeId = 12, CoverageAmount = 2000, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 2000) },
                new InsuranceAddonType { InsuranceAddonTypeId = 13, CoverageAmount = 2500, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 2500) },
                new InsuranceAddonType { InsuranceAddonTypeId = 14, CoverageAmount = 3000, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 3000) },
                new InsuranceAddonType { InsuranceAddonTypeId = 15, CoverageAmount = 3500, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 3500) },
                new InsuranceAddonType { InsuranceAddonTypeId = 16, CoverageAmount = 4000, Description = AddonType.LongTermSickness, BaseExtraPremium = PremiumCalculator.CalculateAddonExtraPremium(AddonType.LongTermSickness, 4000) }
                );
            #endregion
        }

        public static void SeedVehicleCoverageOptions(this ModelBuilder modelBuilder)
        {
            //Seed for VehicleInsuranceCoverageOptions (bilaga för Fordonsförsäkring personbil) 
            #region Seed for VehicleInsuranceCoverageOptions
            modelBuilder.Entity<VehicleInsuranceOption>().HasData(
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 1, Deductible = 1000, OptionDescription = VehicleCoverageOptions.Traffic, OptionCost = 350 },
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 2, Deductible = 1000, OptionDescription = VehicleCoverageOptions.Half, OptionCost = 550 },
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 3, Deductible = 1000, OptionDescription = VehicleCoverageOptions.Full, OptionCost = 800 },

               new VehicleInsuranceOption { VehicleInsuranceOptionId = 4, Deductible = 2000, OptionDescription = VehicleCoverageOptions.Traffic, OptionCost = 300 },
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 5, Deductible = 2000, OptionDescription = VehicleCoverageOptions.Half, OptionCost = 450 },
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 6, Deductible = 2000, OptionDescription = VehicleCoverageOptions.Full, OptionCost = 700 },

               new VehicleInsuranceOption { VehicleInsuranceOptionId = 7, Deductible = 3500, OptionDescription = VehicleCoverageOptions.Traffic, OptionCost = 250 },
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 8, Deductible = 3500, OptionDescription = VehicleCoverageOptions.Half, OptionCost = 400 },
               new VehicleInsuranceOption { VehicleInsuranceOptionId = 9, Deductible = 3500, OptionDescription = VehicleCoverageOptions.Full, OptionCost = 600 }
            );
            #endregion
        }

        public static void SeedLiabilityCoverageOptions(this ModelBuilder modelBuilder)
        {
            #region Seed for LiabilityCoverageOptions
            modelBuilder.Entity<LiabilityCoverageOption>().HasData(
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 1, Deductible = Deductible.Quarter, OptionAmount = LiabilityCoverageOptionAmounts.ThreeMillion, MonthlyPremium = 800 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 2, Deductible = Deductible.Quarter, OptionAmount = LiabilityCoverageOptionAmounts.FiveMillion, MonthlyPremium = 1300 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 3, Deductible = Deductible.Quarter, OptionAmount = LiabilityCoverageOptionAmounts.TenMillion, MonthlyPremium = 1800 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 4, Deductible = Deductible.Half, OptionAmount = LiabilityCoverageOptionAmounts.ThreeMillion, MonthlyPremium = 700 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 5, Deductible = Deductible.Half, OptionAmount = LiabilityCoverageOptionAmounts.FiveMillion, MonthlyPremium = 1200 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 6, Deductible = Deductible.Half, OptionAmount = LiabilityCoverageOptionAmounts.TenMillion, MonthlyPremium = 1700 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 7, Deductible = Deductible.ThreeQuarter, OptionAmount = LiabilityCoverageOptionAmounts.ThreeMillion, MonthlyPremium = 600 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 8, Deductible = Deductible.ThreeQuarter, OptionAmount = LiabilityCoverageOptionAmounts.FiveMillion, MonthlyPremium = 1100 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 9, Deductible = Deductible.ThreeQuarter, OptionAmount = LiabilityCoverageOptionAmounts.TenMillion, MonthlyPremium = 1600 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 10, Deductible = Deductible.Full, OptionAmount = LiabilityCoverageOptionAmounts.ThreeMillion, MonthlyPremium = 500 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 11, Deductible = Deductible.Full, OptionAmount = LiabilityCoverageOptionAmounts.FiveMillion, MonthlyPremium = 900 },
                new LiabilityCoverageOption { LiabilityCoverageOptionId = 12, Deductible = Deductible.Full, OptionAmount = LiabilityCoverageOptionAmounts.TenMillion, MonthlyPremium = 1400 }
                );

            #endregion

        }

        public static void SeedOrganization(this ModelBuilder modelBuilder)
        {
            var employees = new List<Employee>();

            // VD
            var stenHard = new Employee
            {
                FirstName = "Sten",
                LastName = "Hård",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Sten", Last = "Hård" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Sten", "Hård"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Sten", "Hård"),
                Manager = null,
                PersonalNumber = "19700101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.CEO),
                AgentNumber = null,
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.CEO,
                        Percentage = 100
                    }
                }
            };
            employees.Add(stenHard);

            // Ekonomiassistent
            var annSofieLarsson = new Employee
            {
                FirstName = "Ann-Sofie",
                LastName = "Larsson",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Ann-Sofie", Last = "Larsson" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Ann-Sofie", "Larsson"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Ann-Sofie", "Larsson"),
                Manager = stenHard, // Ann-Sofie rapporterar till VD
                PersonalNumber = "19850101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.FinancialAssistant),
                AgentNumber = null,
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.FinancialAssistant,
                        Percentage = 100
                    }
                }
            };
            employees.Add(annSofieLarsson);

            // Försäljningschef
            var irenPanik = new Employee
            {
                FirstName = "Iren",
                LastName = "Panik",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Iren", Last = "Panik" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Iren", "Panik"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Iren", "Panik"),
                Manager = stenHard, // Iren rapporterar till VD
                PersonalNumber = "19820101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.SalesManager),
                AgentNumber = null,
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.SalesManager,
                        Percentage = 100
                    }
                }
            };
            employees.Add(irenPanik);

            // Innesäljare och Utesäljare
            var ireneJohansson = new Employee
            {
                FirstName = "Irene",
                LastName = "Johansson",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Irene", Last = "Johansson" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Irene", "Johansson"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Irene", "Johansson"),
                Manager = irenPanik,
                PersonalNumber = "19810101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.InsideSales),
                AgentNumber = "2547",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.InsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(ireneJohansson);

            var karinSundberg = new Employee
            {
                FirstName = "Karin",
                LastName = "Sundberg",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Karin", Last = "Sundberg" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Karin", "Sundberg"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Karin", "Sundberg"),
                Manager = irenPanik,
                PersonalNumber = "19890101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.SalesAssistant),
                AgentNumber = "6423",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.InsideSales,
                        Percentage = 75
                    },
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.SalesAssistant,
                        Percentage = 25 // Justera enligt vad som passar bäst
                    }
                }
            };
            employees.Add(karinSundberg);

            var vigoPersson = new Employee
            {
                FirstName = "Vigo",
                LastName = "Persson",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Vigo", Last = "Persson" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Vigo", "Persson"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Vigo", "Persson"),
                Manager = irenPanik,
                PersonalNumber = "19900101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.InsideSales),
                AgentNumber = "2447",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.InsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(vigoPersson);

            // Utesäljare
            var birgittaFrisk = new Employee
            {
                FirstName = "Birgitta",
                LastName = "Frisk",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Birgitta", Last = "Frisk" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Birgitta", "Frisk"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Birgitta", "Frisk"),
                Manager = irenPanik,
                PersonalNumber = "19830101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.OutsideSales),
                AgentNumber = "5836",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.OutsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(birgittaFrisk);

            var borisAlm = new Employee
            {
                FirstName = "Boris",
                LastName = "Alm",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Boris", Last = "Alm" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Boris", "Alm"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Boris", "Alm"),
                Manager = irenPanik,
                PersonalNumber = "19750101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.OutsideSales),
                AgentNumber = "2264",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.OutsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(borisAlm);

            var lindaJonsson = new Employee
            {
                FirstName = "Linda",
                LastName = "Jonsson",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Linda", Last = "Jonsson" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Linda", "Jonsson"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Linda", "Jonsson"),
                Manager = irenPanik,
                PersonalNumber = "19850101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.OutsideSales),
                AgentNumber = "1153",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.OutsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(lindaJonsson);

            var malinNilsdotter = new Employee
            {
                FirstName = "Malin",
                LastName = "Nilsdotter",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Malin", Last = "Nilsdotter" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Malin", "Nilsdotter"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Malin", "Nilsdotter"),
                Manager = irenPanik,
                PersonalNumber = "19860101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.OutsideSales),
                AgentNumber = "7473",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.OutsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(malinNilsdotter);

            var mikaelLund = new Employee
            {
                FirstName = "Mikael",
                LastName = "Lund",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Mikael", Last = "Lund" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Mikael", "Lund"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Mikael", "Lund"),
                Manager = irenPanik,
                PersonalNumber = "19870101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.OutsideSales),
                AgentNumber = "4331",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.OutsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(mikaelLund);

            var patrikHedman = new Employee
            {
                FirstName = "Patrik",
                LastName = "Hedman",
                Username = EmployeeHelper.GenerateEmployeeUsername(new Name { First = "Patrik", Last = "Hedman" }),
                Password = EmployeeHelper.GenerateEmployeePassword("Patrik", "Hedman"),
                Email = EmployeeHelper.GenerateEmployeeEmail("Patrik", "Hedman"),
                Manager = irenPanik,
                PersonalNumber = "19880101-0000",
                BaseSalary = EmployeeHelper.GetSalaryForEmployeeType(EmployeeType.OutsideSales),
                AgentNumber = "7337",
                RoleAssignments = new List<EmployeeRoleAssignment>
                {
                    new EmployeeRoleAssignment
                    {
                        Role = EmployeeType.OutsideSales,
                        Percentage = 100
                    }
                }
            };
            employees.Add(patrikHedman);

            // Seed employees to the database
            modelBuilder.Entity<Employee>().HasData(employees);


        }


    }
}
