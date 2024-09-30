using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class UpdatesVehicleCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deductible",
                table: "VehicleInsuranceCoverage",
                newName: "MonthlyPremium");

            migrationBuilder.RenameColumn(
                name: "BaseCost",
                table: "VehicleInsuranceCoverage",
                newName: "CoverageAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyPremium",
                table: "VehicleInsuranceCoverage",
                newName: "Deductible");

            migrationBuilder.RenameColumn(
                name: "CoverageAmount",
                table: "VehicleInsuranceCoverage",
                newName: "BaseCost");
        }
    }
}
