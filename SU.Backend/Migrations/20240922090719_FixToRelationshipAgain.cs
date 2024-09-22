using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class FixToRelationshipAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverages_PrivateCoverageOptionId",
                table: "PrivateCoverages");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_PrivateCoverageOptionId",
                table: "PrivateCoverages",
                column: "PrivateCoverageOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverages_PrivateCoverageOptionId",
                table: "PrivateCoverages");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_PrivateCoverageOptionId",
                table: "PrivateCoverages",
                column: "PrivateCoverageOptionId",
                unique: true);
        }
    }
}
