using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class RemovedFK_RiskZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleCoverageId",
                table: "RizkZone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleCoverageId",
                table: "RizkZone",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
