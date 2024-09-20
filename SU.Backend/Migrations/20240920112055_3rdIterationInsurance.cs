using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class _3rdIterationInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderNr",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "CoverageAmount",
                table: "PrivateCoverage");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "PrivateCoverage");

            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "PrivateCoverage");

            migrationBuilder.DropColumn(
                name: "MonthlyPremium",
                table: "PrivateCoverage");

            migrationBuilder.RenameColumn(
                name: "InsurancePolicyHolderNr",
                table: "Insurances",
                newName: "InsurancePolicyHolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Insurances_InsurancePolicyHolderNr",
                table: "Insurances",
                newName: "IX_Insurances_InsurancePolicyHolderId");

            migrationBuilder.AddColumn<int>(
                name: "PrivateCoverageOptionId",
                table: "PrivateCoverage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrivateCoverageOption",
                columns: table => new
                {
                    PrivateCoverageOptionId = table.Column<int>(type: "int", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateCoverageOption", x => x.PrivateCoverageOptionId);
                    table.ForeignKey(
                        name: "FK_PrivateCoverageOption_PrivateCoverage_PrivateCoverageOptionId",
                        column: x => x.PrivateCoverageOptionId,
                        principalTable: "PrivateCoverage",
                        principalColumn: "PrivateCoverageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "Insurances");

            migrationBuilder.DropTable(
                name: "PrivateCoverageOption");

            migrationBuilder.DropColumn(
                name: "PrivateCoverageOptionId",
                table: "PrivateCoverage");

            migrationBuilder.RenameColumn(
                name: "InsurancePolicyHolderId",
                table: "Insurances",
                newName: "InsurancePolicyHolderNr");

            migrationBuilder.RenameIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances",
                newName: "IX_Insurances_InsurancePolicyHolderNr");

            migrationBuilder.AddColumn<decimal>(
                name: "CoverageAmount",
                table: "PrivateCoverage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "PrivateCoverage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InsuranceType",
                table: "PrivateCoverage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPremium",
                table: "PrivateCoverage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderNr",
                table: "Insurances",
                column: "InsurancePolicyHolderNr",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
