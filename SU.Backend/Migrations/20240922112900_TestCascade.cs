using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class TestCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverages",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId",
                principalTable: "InsuredPersons",
                principalColumn: "InsuredPersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverages",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId",
                principalTable: "InsuredPersons",
                principalColumn: "InsuredPersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
