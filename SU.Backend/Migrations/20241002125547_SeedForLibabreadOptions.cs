using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class SeedForLibabreadOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LiabilityCoverageOption",
                columns: new[] { "LiabilityCoverageOptionId", "Deductible", "MonthlyPremium", "OptionAmount" },
                values: new object[,]
                {
                    { 1, "Quarter", 800m, "ThreeMillion" },
                    { 2, "Quarter", 1300m, "FiveMillion" },
                    { 3, "Quarter", 1800m, "TenMillion" },
                    { 4, "Half", 700m, "ThreeMillion" },
                    { 5, "Half", 1200m, "FiveMillion" },
                    { 6, "Half", 1700m, "TenMillion" },
                    { 7, "ThreeQuarter", 600m, "ThreeMillion" },
                    { 8, "ThreeQuarter", 1100m, "FiveMillion" },
                    { 9, "ThreeQuarter", 1600m, "TenMillion" },
                    { 10, "Full", 500m, "ThreeMillion" },
                    { 11, "Full", 900m, "FiveMillion" },
                    { 12, "Full", 1400m, "TenMillion" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "LiabilityCoverageOption",
                keyColumn: "LiabilityCoverageOptionId",
                keyValue: 12);
        }
    }
}
