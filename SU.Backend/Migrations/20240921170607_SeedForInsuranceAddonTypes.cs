using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class SeedForInsuranceAddonTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CoverageAmount",
                table: "InsuranceAddonTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "InsuranceAddonTypes",
                columns: new[] { "InsuranceAddonTypeId", "BaseExtraPremium", "CoverageAmount", "Description" },
                values: new object[,]
                {
                    { 1, 0m, 10000m, "SicknessAccident" },
                    { 2, 0m, 20000m, "SicknessAccident" },
                    { 3, 0m, 30000m, "SicknessAccident" },
                    { 4, 0m, 40000m, "SicknessAccident" },
                    { 5, 0m, 50000m, "SicknessAccident" },
                    { 6, 0m, 60000m, "SicknessAccident" },
                    { 7, 0m, 70000m, "SicknessAccident" },
                    { 8, 0m, 80000m, "SicknessAccident" },
                    { 9, 0m, 500m, "LongTermSickness" },
                    { 10, 0m, 1000m, "LongTermSickness" },
                    { 11, 0m, 1500m, "LongTermSickness" },
                    { 12, 0m, 2000m, "LongTermSickness" },
                    { 13, 0m, 2500m, "LongTermSickness" },
                    { 14, 0m, 3000m, "LongTermSickness" },
                    { 15, 0m, 3500m, "LongTermSickness" },
                    { 16, 0m, 4000m, "LongTermSickness" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "InsuranceAddonTypes",
                keyColumn: "InsuranceAddonTypeId",
                keyValue: 16);

            migrationBuilder.DropColumn(
                name: "CoverageAmount",
                table: "InsuranceAddonTypes");
        }
    }
}
