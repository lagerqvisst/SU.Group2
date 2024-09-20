using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class UpdatesToCardinality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskZoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances",
                column: "InsuranceCoverageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances",
                column: "InsuranceCoverageId");
        }
    }
}
