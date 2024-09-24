using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class ExtendRiskZone_Multiplicity_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.AddColumn<int>(
                name: "RizkZoneRiskZoneId",
                table: "VehicleInsuranceCoverage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RizkZoneRiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RizkZoneRiskZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverage_RizkZone_RizkZoneRiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RizkZoneRiskZoneId",
                principalTable: "RizkZone",
                principalColumn: "RiskZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverage_RizkZone_RizkZoneRiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropIndex(
                name: "IX_VehicleInsuranceCoverage_RizkZoneRiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropColumn(
                name: "RizkZoneRiskZoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInsuranceCoverage_RiskZoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskZoneId",
                unique: true);
        }
    }
}
