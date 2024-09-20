using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class InitialCreate_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseSalary = table.Column<int>(type: "int", nullable: false),
                    ManagerEmployeeId = table.Column<int>(type: "int", nullable: true),
                    AgentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerEmployeeId",
                        column: x => x.ManagerEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceAddonTypes",
                columns: table => new
                {
                    InsuranceAddonTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseExtraPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceAddonTypes", x => x.InsuranceAddonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCoverages",
                columns: table => new
                {
                    InsuranceCoverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCoverages", x => x.InsuranceCoverageId);
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
                name: "RizkZone",
                columns: table => new
                {
                    RiskZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskZoneLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZoneFactor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RizkZone", x => x.RiskZoneId);
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
                        principalColumn: "CompanyCustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancePolicyHolders_PrivateCustomers_PrivateCustomerId",
                        column: x => x.PrivateCustomerId,
                        principalTable: "PrivateCustomers",
                        principalColumn: "PrivateCustomerId",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyHolderId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                        column: x => x.InsurancePolicyHolderId,
                        principalTable: "InsurancePolicyHolders",
                        principalColumn: "InsurancePolicyHolderId",
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
                    InsurancePolicyHolderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuredPersons", x => x.InsuredPersonId);
                    table.ForeignKey(
                        name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                        column: x => x.InsurancePolicyHolderId,
                        principalTable: "InsurancePolicyHolders",
                        principalColumn: "InsurancePolicyHolderId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceAddons",
                columns: table => new
                {
                    InsuranceAddonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceAddonTypeId = table.Column<int>(type: "int", nullable: false),
                    PrivateInsuranceId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_InsuranceAddons_Insurances_PrivateInsuranceId",
                        column: x => x.PrivateInsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateCoverage",
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
                    table.ForeignKey(
                        name: "FK_PrivateCoverage_PrivateCoverageOption_PrivateCoverageOptionId",
                        column: x => x.PrivateCoverageOptionId,
                        principalTable: "PrivateCoverageOption",
                        principalColumn: "PrivateCoverageOptionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRoleAssignments_EmployeeId",
                table: "EmployeeRoleAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerEmployeeId",
                table: "Employees",
                column: "ManagerEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceAddons_InsuranceAddonTypeId",
                table: "InsuranceAddons",
                column: "InsuranceAddonTypeId");

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
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuredPersons_InsurancePolicyHolderId",
                table: "InsuredPersons",
                column: "InsurancePolicyHolderId");

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
                name: "IX_PrivateCoverage_PrivateCoverageOptionId",
                table: "PrivateCoverage",
                column: "PrivateCoverageOptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAndInventoryCoverage_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_InsuranceCoverageId",
                table: "VehicleInsuranceCoverage",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskZoneId",
                unique: true);
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
                name: "PrivateCoverage");

            migrationBuilder.DropTable(
                name: "PropertyAndInventoryCoverage");

            migrationBuilder.DropTable(
                name: "VehicleInsuranceCoverage");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "InsuranceAddonTypes");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "InsuredPersons");

            migrationBuilder.DropTable(
                name: "PrivateCoverageOption");

            migrationBuilder.DropTable(
                name: "RizkZone");

            migrationBuilder.DropTable(
                name: "InsuranceCoverages");

            migrationBuilder.DropTable(
                name: "InsurancePolicyHolders");

            migrationBuilder.DropTable(
                name: "CompanyCustomer");

            migrationBuilder.DropTable(
                name: "PrivateCustomers");
        }
    }
}
