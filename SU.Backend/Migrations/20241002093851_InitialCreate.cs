using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyCustomers",
                columns: table => new
                {
                    CompanyCustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_CompanyCustomers", x => x.CompanyCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseSalary = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    AgentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceAddonTypes",
                columns: table => new
                {
                    InsuranceAddonTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaseExtraPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceAddonTypes", x => x.InsuranceAddonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "InsuredPersons",
                columns: table => new
                {
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuredPersons", x => x.InsuredPersonId);
                });

            migrationBuilder.CreateTable(
                name: "PrivateCoverageOption",
                columns: table => new
                {
                    PrivateCoverageOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateCoverageOption", x => x.PrivateCoverageOptionId);
                });

            migrationBuilder.CreateTable(
                name: "PrivateCustomers",
                columns: table => new
                {
                    PrivateCustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateCustomers", x => x.PrivateCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Riskzones",
                columns: table => new
                {
                    RiskzoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskzoneLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZoneFactor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riskzones", x => x.RiskzoneId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleInsuranceOptions",
                columns: table => new
                {
                    VehicleInsuranceOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deductible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OptionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInsuranceOptions", x => x.VehicleInsuranceOptionId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoleAssignments",
                columns: table => new
                {
                    EmployeeRoleAssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoleAssignments", x => x.EmployeeRoleAssignmentId);
                    table.ForeignKey(
                        name: "FK_EmployeeRoleAssignments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_InsurancePolicyHolders_CompanyCustomers_CompanyCustomerId",
                        column: x => x.CompanyCustomerId,
                        principalTable: "CompanyCustomers",
                        principalColumn: "CompanyCustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsurancePolicyHolders_PrivateCustomers_PrivateCustomerId",
                        column: x => x.PrivateCustomerId,
                        principalTable: "PrivateCustomers",
                        principalColumn: "PrivateCustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prospects",
                columns: table => new
                {
                    ProspectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProspectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProspectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedAgentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateCustomerId = table.Column<int>(type: "int", nullable: true),
                    CompanyCustomerId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospects", x => x.ProspectId);
                    table.ForeignKey(
                        name: "FK_Prospects_CompanyCustomers_CompanyCustomerId",
                        column: x => x.CompanyCustomerId,
                        principalTable: "CompanyCustomers",
                        principalColumn: "CompanyCustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_Employees_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_PrivateCustomers_PrivateCustomerId",
                        column: x => x.PrivateCustomerId,
                        principalTable: "PrivateCustomers",
                        principalColumn: "PrivateCustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyHolderId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    Premium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                        name: "FK_Insurances_Employees_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                        column: x => x.InsurancePolicyHolderId,
                        principalTable: "InsurancePolicyHolders",
                        principalColumn: "InsurancePolicyHolderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceAddons",
                columns: table => new
                {
                    InsuranceAddonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceAddonTypeId = table.Column<int>(type: "int", nullable: false),
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceAddons", x => x.InsuranceAddonId);
                    table.ForeignKey(
                        name: "FK_InsuranceAddons_InsuranceAddonTypes_InsuranceAddonTypeId",
                        column: x => x.InsuranceAddonTypeId,
                        principalTable: "InsuranceAddonTypes",
                        principalColumn: "InsuranceAddonTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceAddons_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCoverages",
                columns: table => new
                {
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCoverages", x => x.InsuranceCoverageId);
                    table.ForeignKey(
                        name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateCoverages",
                columns: table => new
                {
                    PrivateCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false),
                    PrivateCoverageOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateCoverages", x => x.PrivateCoverageId);
                    table.ForeignKey(
                        name: "FK_PrivateCoverages_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                        column: x => x.InsuredPersonId,
                        principalTable: "InsuredPersons",
                        principalColumn: "InsuredPersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivateCoverages_PrivateCoverageOption_PrivateCoverageOptionId",
                        column: x => x.PrivateCoverageOptionId,
                        principalTable: "PrivateCoverageOption",
                        principalColumn: "PrivateCoverageOptionId",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleInsuranceCoverage",
                columns: table => new
                {
                    VehicleInsuranceCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false),
                    RiskzoneId = table.Column<int>(type: "int", nullable: false),
                    VehicleInsuranceOptionId = table.Column<int>(type: "int", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInsuranceCoverage", x => x.VehicleInsuranceCoverageId);
                    table.ForeignKey(
                        name: "FK_VehicleInsuranceCoverage_InsuranceCoverages_InsuranceCoverageId",
                        column: x => x.InsuranceCoverageId,
                        principalTable: "InsuranceCoverages",
                        principalColumn: "InsuranceCoverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleInsuranceCoverage_Riskzones_RiskzoneId",
                        column: x => x.RiskzoneId,
                        principalTable: "Riskzones",
                        principalColumn: "RiskzoneId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                        column: x => x.VehicleInsuranceOptionId,
                        principalTable: "VehicleInsuranceOptions",
                        principalColumn: "VehicleInsuranceOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "InsuranceAddonTypes",
                columns: new[] { "InsuranceAddonTypeId", "BaseExtraPremium", "CoverageAmount", "Description" },
                values: new object[,]
                {
                    { 1, 3.0000m, 100000m, "SicknessAccident" },
                    { 2, 6.0000m, 200000m, "SicknessAccident" },
                    { 3, 9.0000m, 300000m, "SicknessAccident" },
                    { 4, 12.0000m, 400000m, "SicknessAccident" },
                    { 5, 15.0000m, 500000m, "SicknessAccident" },
                    { 6, 18.0000m, 600000m, "SicknessAccident" },
                    { 7, 21.0000m, 700000m, "SicknessAccident" },
                    { 8, 24.0000m, 800000m, "SicknessAccident" },
                    { 9, 0.2500m, 500m, "LongTermSickness" },
                    { 10, 0.5000m, 1000m, "LongTermSickness" },
                    { 11, 0.7500m, 1500m, "LongTermSickness" },
                    { 12, 1.0000m, 2000m, "LongTermSickness" },
                    { 13, 1.2500m, 2500m, "LongTermSickness" },
                    { 14, 1.5000m, 3000m, "LongTermSickness" },
                    { 15, 1.7500m, 3500m, "LongTermSickness" },
                    { 16, 2.0000m, 4000m, "LongTermSickness" }
                });

            migrationBuilder.InsertData(
                table: "PrivateCoverageOption",
                columns: new[] { "PrivateCoverageOptionId", "CoverageAmount", "InsuranceType", "MonthlyPremium", "StartDate" },
                values: new object[,]
                {
                    { 1, 700000m, "ChildAccidentAndHealthInsurance", 350m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
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

            migrationBuilder.InsertData(
                table: "Riskzones",
                columns: new[] { "RiskzoneId", "RiskzoneLevel", "ZoneFactor" },
                values: new object[,]
                {
                    { 1, "Zone1", 1.3 },
                    { 2, "Zone2", 1.2 },
                    { 3, "Zone3", 1.1000000000000001 },
                    { 4, "Zone4", 1.0 }
                });

            migrationBuilder.InsertData(
                table: "VehicleInsuranceOptions",
                columns: new[] { "VehicleInsuranceOptionId", "Deductible", "OptionCost", "OptionDescription" },
                values: new object[,]
                {
                    { 1, 1000m, 350m, "Traffic" },
                    { 2, 1000m, 550m, "Half" }
                });

            migrationBuilder.InsertData(
                table: "VehicleInsuranceOptions",
                columns: new[] { "VehicleInsuranceOptionId", "Deductible", "OptionCost", "OptionDescription" },
                values: new object[,]
                {
                    { 3, 1000m, 800m, "Full" },
                    { 4, 2000m, 300m, "Traffic" },
                    { 5, 2000m, 450m, "Half" },
                    { 6, 2000m, 700m, "Full" },
                    { 7, 3500m, 250m, "Traffic" },
                    { 8, 3500m, 400m, "Half" },
                    { 9, 3500m, 600m, "Full" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoleAssignments_EmployeeId",
                table: "EmployeeRoleAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceAddons_InsuranceAddonTypeId",
                table: "InsuranceAddons",
                column: "InsuranceAddonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceAddons_InsuranceId",
                table: "InsuranceAddons",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCoverages_InsuranceId",
                table: "InsuranceCoverages",
                column: "InsuranceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicyHolders_CompanyCustomerId",
                table: "InsurancePolicyHolders",
                column: "CompanyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicyHolders_PrivateCustomerId",
                table: "InsurancePolicyHolders",
                column: "PrivateCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_SellerId",
                table: "Insurances",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityCoverage_InsuranceCoverageId",
                table: "LiabilityCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_InsuranceCoverageId",
                table: "PrivateCoverages",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_PrivateCoverageOptionId",
                table: "PrivateCoverages",
                column: "PrivateCoverageOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAndInventoryCoverage_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_CompanyCustomerId",
                table: "Prospects",
                column: "CompanyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_PrivateCustomerId",
                table: "Prospects",
                column: "PrivateCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_SellerId",
                table: "Prospects",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_InsuranceCoverageId",
                table: "VehicleInsuranceCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskzoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskzoneId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage",
                column: "VehicleInsuranceOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoleAssignments");

            migrationBuilder.DropTable(
                name: "InsuranceAddons");

            migrationBuilder.DropTable(
                name: "LiabilityCoverage");

            migrationBuilder.DropTable(
                name: "PrivateCoverages");

            migrationBuilder.DropTable(
                name: "PropertyAndInventoryCoverage");

            migrationBuilder.DropTable(
                name: "Prospects");

            migrationBuilder.DropTable(
                name: "VehicleInsuranceCoverage");

            migrationBuilder.DropTable(
                name: "InsuranceAddonTypes");

            migrationBuilder.DropTable(
                name: "InsuredPersons");

            migrationBuilder.DropTable(
                name: "PrivateCoverageOption");

            migrationBuilder.DropTable(
                name: "InsuranceCoverages");

            migrationBuilder.DropTable(
                name: "Riskzones");

            migrationBuilder.DropTable(
                name: "VehicleInsuranceOptions");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "InsurancePolicyHolders");

            migrationBuilder.DropTable(
                name: "CompanyCustomers");

            migrationBuilder.DropTable(
                name: "PrivateCustomers");
        }
    }
}
