using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Added_FK_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceCoverageId",
                table: "Insurances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "InsuranceCoverageId",
                table: "Insurances");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsurancePolicyHolderId",
                table: "Insurances",
                column: "InsurancePolicyHolderId");
        }
    }
}
