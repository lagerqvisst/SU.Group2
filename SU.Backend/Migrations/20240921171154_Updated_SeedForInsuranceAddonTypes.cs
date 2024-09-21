using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Updated_SeedForInsuranceAddonTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 1,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 3.0000m, 100000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 2,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 6.0000m, 200000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 3,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 9.0000m, 300000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 4,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 12.0000m, 400000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 5,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 15.0000m, 500000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 6,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 18.0000m, 600000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 7,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 21.0000m, 700000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 8,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 24.0000m, 800000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 9,
                column: "BaseExtraPremium",
                value: 0.2500m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 10,
                column: "BaseExtraPremium",
                value: 0.5000m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 11,
                column: "BaseExtraPremium",
                value: 0.7500m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 12,
                column: "BaseExtraPremium",
                value: 1.0000m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 13,
                column: "BaseExtraPremium",
                value: 1.2500m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 14,
                column: "BaseExtraPremium",
                value: 1.5000m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 15,
                column: "BaseExtraPremium",
                value: 1.7500m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 16,
                column: "BaseExtraPremium",
                value: 2.0000m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 1,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 10000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 2,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 20000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 3,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 30000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 4,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 40000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 5,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 50000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 6,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 60000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 7,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 70000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 8,
                columns: new[] { "BaseExtraPremium", "CoverageAmount" },
                values: new object[] { 0m, 80000m });

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 9,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 10,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 11,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 12,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 13,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 14,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 15,
                column: "BaseExtraPremium",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 16,
                column: "BaseExtraPremium",
                value: 0m);
        }
    }
}
