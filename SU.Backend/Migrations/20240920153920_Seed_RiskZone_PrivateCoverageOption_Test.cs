using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Seed_RiskZone_PrivateCoverageOption_Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PrivateCoverageOption",
                columns: new[] { "PrivateCoverageOptionId", "CoverageAmount", "InsuranceType", "MonthlyPremium", "StartDate" },
                values: new object[] { 1, 700000m, "ChildAccidentAndHealthInsurance", 350m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "RizkZone",
                columns: new[] { "RiskZoneId", "RiskZoneLevel", "ZoneFactor" },
                values: new object[,]
                {
                    { 1, "Zone1", 1.3 },
                    { 2, "Zone2", 1.2 },
                    { 3, "Zone3", 1.1000000000000001 },
                    { 4, "Zone4", 1.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RizkZone",
                keyColumn: "RiskZoneId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RizkZone",
                keyColumn: "RiskZoneId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RizkZone",
                keyColumn: "RiskZoneId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RizkZone",
                keyColumn: "RiskZoneId",
                keyValue: 4);
        }
    }
}
