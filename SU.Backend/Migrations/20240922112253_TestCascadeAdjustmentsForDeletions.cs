using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class TestCascadeAdjustmentsForDeletions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                table: "InsuranceCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "Insurances");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                table: "InsuranceCoverages",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                table: "InsuranceCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "Insurances");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                table: "InsuranceCoverages",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_InsurancePolicyHolders_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId",
                principalTable: "InsurancePolicyHolders",
                principalColumn: "InsurancePolicyHolderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
