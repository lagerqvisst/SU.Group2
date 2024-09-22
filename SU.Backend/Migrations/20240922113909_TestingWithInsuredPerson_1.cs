using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class TestingWithInsuredPerson_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.DropColumn(
                name: "PrivateCoverageId",
                table: "InsuredPersons");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.AddColumn<int>(
                name: "PrivateCoverageId",
                table: "InsuredPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId",
                unique: true);
        }
    }
}
