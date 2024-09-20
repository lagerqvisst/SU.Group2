using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class InsuranceDraftWithPrivateCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateCustomer",
                table: "PrivateCustomer");

            migrationBuilder.RenameTable(
                name: "PrivateCustomer",
                newName: "PrivateCustomers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateCustomers",
                table: "PrivateCustomers",
                column: "PrivateCustomerId");

            migrationBuilder.CreateTable(
                name: "CompanyCustomer",
                columns: table => new
                {
                    CompanyCustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonPhonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLandlineNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCustomer", x => x.CompanyCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCoverages",
                columns: table => new
                {
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCoverages", x => x.InsuranceCoverageId);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicyHolders",
                columns: table => new
                {
                    InsurancePolicyHolderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCustomerId = table.Column<int>(type: "int", nullable: true),
                    PrivateCustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicyHolders", x => x.InsurancePolicyHolderId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                        column: x => x.CompanyCustomerId,
                        principalTable: "CompanyCustomer",
                        principalColumn: "CompanyCustomerId");
                    table.ForeignKey(
                        name: "FK_InsurancePolicyHolders_PrivateCustomers_PrivateCustomerId",
                        column: x => x.PrivateCustomerId,
                        principalTable: "PrivateCustomers",
                        principalColumn: "PrivateCustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuredPersons",
                columns: table => new
                {
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsurancePolicyHolderNr = table.Column<int>(type: "int", nullable: false),
                    InsurancePolicyHolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuredPersons", x => x.InsuredPersonId);
                    table.ForeignKey(
                        name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                        column: x => x.InsurancePolicyHolderId,
                        principalTable: "InsurancePolicyHolders",
                        principalColumn: "InsurancePolicyHolderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateInsurances",
                columns: table => new
                {
                    PrivateInsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyHolderNr = table.Column<int>(type: "int", nullable: false),
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    InsuranceStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateInsurances", x => x.PrivateInsuranceId);
                    table.ForeignKey(
                        name: "FK_PrivateInsurances_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivateInsurances_InsurancePolicyHolders_InsurancePolicyHolderNr",
                        column: x => x.InsurancePolicyHolderNr,
                        principalTable: "InsurancePolicyHolders",
                        principalColumn: "InsurancePolicyHolderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivateInsurances_InsuredPersons_InsuredPersonId",
                        column: x => x.InsuredPersonId,
                        principalTable: "InsuredPersons",
                        principalColumn: "InsuredPersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceAddons",
                columns: table => new
                {
                    InsuranceAddonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceAddonType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SicknessAccidentOption = table.Column<int>(type: "int", nullable: true),
                    LongTermSicknessOption = table.Column<int>(type: "int", nullable: true),
                    ExtraPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrivateInsuranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceAddons", x => x.InsuranceAddonId);
                    table.ForeignKey(
                        name: "FK_InsuranceAddons_PrivateInsurances_PrivateInsuranceId",
                        column: x => x.PrivateInsuranceId,
                        principalTable: "PrivateInsurances",
                        principalColumn: "PrivateInsuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceAddons_PrivateInsuranceId",
                table: "InsuranceAddons",
                column: "PrivateInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicyHolders_CompanyCustomerId",
                table: "InsurancePolicyHolders",
                column: "CompanyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicyHolders_PrivateCustomerId",
                table: "InsurancePolicyHolders",
                column: "PrivateCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuredPersons_InsurancePolicyHolderId",
                table: "InsuredPersons",
                column: "InsurancePolicyHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateInsurances_InsuranceCoverageId",
                table: "PrivateInsurances",
                column: "InsuranceCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateInsurances_InsurancePolicyHolderNr",
                table: "PrivateInsurances",
                column: "InsurancePolicyHolderNr");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateInsurances_InsuredPersonId",
                table: "PrivateInsurances",
                column: "InsuredPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceAddons");

            migrationBuilder.DropTable(
                name: "PrivateInsurances");

            migrationBuilder.DropTable(
                name: "InsuranceCoverages");

            migrationBuilder.DropTable(
                name: "InsuredPersons");

            migrationBuilder.DropTable(
                name: "InsurancePolicyHolders");

            migrationBuilder.DropTable(
                name: "CompanyCustomer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateCustomers",
                table: "PrivateCustomers");

            migrationBuilder.RenameTable(
                name: "PrivateCustomers",
                newName: "PrivateCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateCustomer",
                table: "PrivateCustomer",
                column: "PrivateCustomerId");
        }
    }
}
