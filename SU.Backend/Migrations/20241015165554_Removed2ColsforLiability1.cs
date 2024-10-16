using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Removed2ColsforLiability1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverageAmount",
                table: "LiabilityCoverage");

            migrationBuilder.DropColumn(
                name: "MonthlyPremium",
                table: "LiabilityCoverage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CoverageAmount",
                table: "LiabilityCoverage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPremium",
                table: "LiabilityCoverage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
