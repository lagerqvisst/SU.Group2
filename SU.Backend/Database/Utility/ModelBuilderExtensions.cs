using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SU.Backend.Models.Enums;
using SU.Backend.Models.Enums.Insurance;
using SU.Backend.Models.Insurance;
using SU.Backend.Models.Insurance.Coverage;
using System;

namespace SU.Backend.Database.Utility
{
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

        public static void ConfigureDecimals(this ModelBuilder modelBuilder)
        {
            var decimalTypes = new[]
            {
                (typeof(InsuranceAddonType), new[] { "BaseExtraPremium" }),
                (typeof(LiabilityCoverage), new[] { "MonthlyPremium", "CoverageAmount" }),
                (typeof(PrivateCoverageOption), new[] { "MonthlyPremium", "CoverageAmount" }),
                (typeof(VehicleInsuranceCoverage), new[] {"BaseCost" , "Deductible" }),
                (typeof(PropertyAndInventoryCoverage), new[] { "PropertyPremium", "PropertyValue", "InventoryPremium", "InventoryValue" })
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

        public static void SeedRiskZones(this ModelBuilder modelBuilder)
        {
            // Seed för RizkZone
            modelBuilder.Entity<RizkZone>().HasData(
                new RizkZone { RiskZoneId = 1, RiskZoneLevel = RiskZoneLevel.Zone1, ZoneFactor = 1.3 },
                new RizkZone { RiskZoneId = 2, RiskZoneLevel = RiskZoneLevel.Zone2, ZoneFactor = 1.2 },
                new RizkZone { RiskZoneId = 3, RiskZoneLevel = RiskZoneLevel.Zone3, ZoneFactor = 1.1 },
                new RizkZone { RiskZoneId = 4, RiskZoneLevel = RiskZoneLevel.Zone4, ZoneFactor = 1.0 }
            );
        }

        public static void SeedPrivateCoverageOptions(this ModelBuilder modelBuilder)
        {
            //Child Accident and Health Insurance
            #region
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
    }
}
