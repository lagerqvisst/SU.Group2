using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SU.Backend.Models.Insurance;
using SU.Backend.Models.Insurance.Coverage;
using System;

namespace SU.Backend.Helper
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
    }
}
