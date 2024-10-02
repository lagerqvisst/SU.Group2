using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class AddedLibabreadOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiabilityCoverageOptionId",
                table: "LiabilityCoverage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LiabilityCoverageOption",
                columns: table => new
                {
                    LiabilityCoverageOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deductible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityCoverageOption", x => x.LiabilityCoverageOptionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityCoverage_LiabilityCoverageOptionId",
                table: "LiabilityCoverage",
                column: "LiabilityCoverageOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityCoverage_LiabilityCoverageOption_LiabilityCoverageOptionId",
                table: "LiabilityCoverage",
                column: "LiabilityCoverageOptionId",
                principalTable: "LiabilityCoverageOption",
                principalColumn: "LiabilityCoverageOptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityCoverage_LiabilityCoverageOption_LiabilityCoverageOptionId",
                table: "LiabilityCoverage");

            migrationBuilder.DropTable(
                name: "LiabilityCoverageOption");

            migrationBuilder.DropIndex(
                name: "IX_LiabilityCoverage_LiabilityCoverageOptionId",
                table: "LiabilityCoverage");

            migrationBuilder.DropColumn(
                name: "LiabilityCoverageOptionId",
                table: "LiabilityCoverage");
        }
    }
}
