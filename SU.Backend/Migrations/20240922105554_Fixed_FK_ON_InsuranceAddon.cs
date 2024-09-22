using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Fixed_FK_ON_InsuranceAddon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceAddons_Insurances_PrivateInsuranceId",
                table: "InsuranceAddons");

            migrationBuilder.RenameColumn(
                name: "PrivateInsuranceId",
                table: "InsuranceAddons",
                newName: "InsuranceId");

            migrationBuilder.RenameIndex(
                name: "IX_InsuranceAddons_PrivateInsuranceId",
                table: "InsuranceAddons",
                newName: "IX_InsuranceAddons_InsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceAddons_Insurances_InsuranceId",
                table: "InsuranceAddons",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceAddons_Insurances_InsuranceId",
                table: "InsuranceAddons");

            migrationBuilder.RenameColumn(
                name: "InsuranceId",
                table: "InsuranceAddons",
                newName: "PrivateInsuranceId");

            migrationBuilder.RenameIndex(
                name: "IX_InsuranceAddons_InsuranceId",
                table: "InsuranceAddons",
                newName: "IX_InsuranceAddons_PrivateInsuranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceAddons_Insurances_PrivateInsuranceId",
                table: "InsuranceAddons",
                column: "PrivateInsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
