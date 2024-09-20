using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class _2ndIteration_Insurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceAddons_PrivateInsurances_PrivateInsuranceId",
                table: "InsuranceAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                table: "InsurancePolicyHolders");

            migrationBuilder.DropTable(
                name: "PrivateInsurances");

            migrationBuilder.DropColumn(
                name: "BaseAmount",
                table: "InsuranceCoverages");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "InsuranceCoverages");

            migrationBuilder.DropColumn(
                name: "InsuranceType",
                table: "InsuranceCoverages");

            migrationBuilder.DropColumn(
                name: "MonthlyPremium",
                table: "InsuranceCoverages");

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyHolderNr = table.Column<int>(type: "int", nullable: false),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsuranceId);
                    table.ForeignKey(
                        name: "FK_Insurances_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderNr",
                        column: x => x.InsurancePolicyHolderNr,
                        principalTable: "InsurancePolicyHolders",
                        principalColumn: "InsurancePolicyHolderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiabilityCoverage",
                columns: table => new
                {
                    LiabilityCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityCoverage", x => x.LiabilityCoverageId);
                    table.ForeignKey(
                        name: "FK_LiabilityCoverage_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PrivateCoverage",
                columns: table => new
                {
                    PrivateCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateCoverage", x => x.PrivateCoverageId);
                    table.ForeignKey(
                        name: "FK_PrivateCoverage_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivateCoverage_InsuredPersons_InsuredPersonId",
                        column: x => x.InsuredPersonId,
                        principalTable: "InsuredPersons",
                        principalColumn: "InsuredPersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAndInventoryCoverage",
                columns: table => new
                {
                    PropertyAndInventoryCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PropertyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InventoryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InventoryPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAndInventoryCoverage", x => x.PropertyAndInventoryCoverageId);
                    table.ForeignKey(
                        name: "FK_PropertyAndInventoryCoverage_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RizkZone",
                columns: table => new
                {
                    RiskZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleCoverageId = table.Column<int>(type: "int", nullable: false),
                    ZoneFactor = table.Column<double>(type: "float", nullable: false),
                    VehicleInsuranceCoverageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RizkZone", x => x.RiskZoneId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleInsuranceCoverage",
                columns: table => new
                {
                    VehicleInsuranceCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    RiskZoneId = table.Column<int>(type: "int", nullable: false),
                    BaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deductible = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInsuranceCoverage", x => x.VehicleInsuranceCoverageId);
                    table.ForeignKey(
                        name: "FK_VehicleInsuranceCoverage_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleInsuranceCoverage_RizkZone_RiskZoneId",
                        column: x => x.RiskZoneId,
                        principalTable: "RizkZone",
                        principalColumn: "RiskZoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances",
                column: "InsuranceCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsurancePolicyHolderNr",
                table: "Insurances",
                column: "InsurancePolicyHolderNr");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityCoverage_InsuranceCoverageId",
                table: "LiabilityCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverage_InsuranceCoverageId",
                table: "PrivateCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverage_InsuredPersonId",
                table: "PrivateCoverage",
                column: "InsuredPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAndInventoryCoverage_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RizkZone_VehicleInsuranceCoverageId",
                table: "RizkZone",
                column: "VehicleInsuranceCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_InsuranceCoverageId",
                table: "VehicleInsuranceCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceAddons_Insurances_PrivateInsuranceId",
                table: "InsuranceAddons",
                column: "PrivateInsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                table: "InsurancePolicyHolders",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomer",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RizkZone_VehicleInsuranceCoverage_VehicleInsuranceCoverageId",
                table: "RizkZone",
                column: "VehicleInsuranceCoverageId",
                principalTable: "VehicleInsuranceCoverage",
                principalColumn: "VehicleInsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceAddons_Insurances_PrivateInsuranceId",
                table: "InsuranceAddons");

            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                table: "InsurancePolicyHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_RizkZone_VehicleInsuranceCoverage_VehicleInsuranceCoverageId",
                table: "RizkZone");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "LiabilityCoverage");

            migrationBuilder.DropTable(
                name: "PrivateCoverage");

            migrationBuilder.DropTable(
                name: "PropertyAndInventoryCoverage");

            migrationBuilder.DropTable(
                name: "VehicleInsuranceCoverage");

            migrationBuilder.DropTable(
                name: "RizkZone");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseAmount",
                table: "InsuranceCoverages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "InsuranceCoverages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InsuranceType",
                table: "InsuranceCoverages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPremium",
                table: "InsuranceCoverages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PrivateInsurances",
                columns: table => new
                {
                    PrivateInsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    InsurancePolicyHolderNr = table.Column<int>(type: "int", nullable: false),
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsuranceStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceAddons_PrivateInsurances_PrivateInsuranceId",
                table: "InsuranceAddons",
                column: "PrivateInsuranceId",
                principalTable: "PrivateInsurances",
                principalColumn: "PrivateInsuranceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                table: "InsurancePolicyHolders",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomer",
                principalColumn: "CompanyCustomerId");
        }
    }
}
