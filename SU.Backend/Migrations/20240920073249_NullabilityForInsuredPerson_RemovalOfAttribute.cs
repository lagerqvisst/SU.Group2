using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class NullabilityForInsuredPerson_RemovalOfAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "InsuredPersons");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyHolderNr",
                table: "InsuredPersons");

            migrationBuilder.AlterColumn<int>(
                name: "InsurancePolicyHolderId",
                table: "InsuredPersons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "InsuredPersons",
                column: "InsurancePolicyHolderId",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "InsuredPersons");

            migrationBuilder.AlterColumn<int>(
                name: "InsurancePolicyHolderId",
                table: "InsuredPersons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsurancePolicyHolderNr",
                table: "InsuredPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_InsuredPersons_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "InsuredPersons",
                column: "InsurancePolicyHolderId",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
