using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Removed_Relation_InsuredPerson_InsurancePolicyHolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "InsuredPersons");

            migrationBuilder.DropIndex(
                name: "IX_InsuredPersons_InsurancePolicyHolderId",
                table: "InsuredPersons");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyHolderId",
                table: "InsuredPersons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsurancePolicyHolderId",
                table: "InsuredPersons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuredPersons_InsurancePolicyHolderId",
                table: "InsuredPersons",
                column: "InsurancePolicyHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "InsuredPersons",
                column: "InsurancePolicyHolderId",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId");
        }
    }
}
