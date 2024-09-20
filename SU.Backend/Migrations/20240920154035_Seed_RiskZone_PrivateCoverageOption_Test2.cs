using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Seed_RiskZone_PrivateCoverageOption_Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PrivateCoverageOption",
                columns: new[] { "PrivateCoverageOptionId", "CoverageAmount", "InsuranceType", "MonthlyPremium", "StartDate" },
                values: new object[,]
                {
                    { 2, 900000m, "ChildAccidentAndHealthInsurance", 450m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1100000m, "ChildAccidentAndHealthInsurance", 550m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1300000m, "ChildAccidentAndHealthInsurance", 650m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 750000m, "ChildAccidentAndHealthInsurance", 375m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 950000m, "ChildAccidentAndHealthInsurance", 475m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1150000m, "ChildAccidentAndHealthInsurance", 575m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1350000m, "ChildAccidentAndHealthInsurance", 675m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 300000m, "AdultAccidentAndHealthInsurance", 150m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 400000m, "AdultAccidentAndHealthInsurance", 200m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 500000m, "AdultAccidentAndHealthInsurance", 250m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 350000m, "AdultAccidentAndHealthInsurance", 175m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 450000m, "AdultAccidentAndHealthInsurance", 225m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 550000m, "AdultAccidentAndHealthInsurance", 275m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 300000m, "AdultLifeInsurance", 150m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 400000m, "AdultLifeInsurance", 200m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 500000m, "AdultLifeInsurance", 250m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 350000m, "AdultLifeInsurance", 175m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 450000m, "AdultLifeInsurance", 225m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 550000m, "AdultLifeInsurance", 275m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PrivateCoverageOption",
                keyColumn: "PrivateCoverageOptionId",
                keyValue: 20);
        }
    }
}
