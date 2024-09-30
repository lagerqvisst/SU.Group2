using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class UpdateToTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOption_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleInsuranceOption",
                table: "VehicleInsuranceOption");

            migrationBuilder.RenameTable(
                name: "VehicleInsuranceOption",
                newName: "VehicleInsuranceOptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleInsuranceOptions",
                table: "VehicleInsuranceOptions",
                column: "VehicleInsuranceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage",
                column: "VehicleInsuranceOptionId",
                principalTable: "VehicleInsuranceOptions",
                principalColumn: "VehicleInsuranceOptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleInsuranceOptions",
                table: "VehicleInsuranceOptions");

            migrationBuilder.RenameTable(
                name: "VehicleInsuranceOptions",
                newName: "VehicleInsuranceOption");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleInsuranceOption",
                table: "VehicleInsuranceOption",
                column: "VehicleInsuranceOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOption_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage",
                column: "VehicleInsuranceOptionId",
                principalTable: "VehicleInsuranceOption",
                principalColumn: "VehicleInsuranceOptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
