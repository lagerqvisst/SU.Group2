using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class UpdatedFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_InsuranceCoverages_InsuranceCoverageId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverage_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverage_PrivateCoverageOption_PrivateCoverageOptionId",
                table: "PrivateCoverage");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateCoverage",
                table: "PrivateCoverage");

            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverage_InsuredPersonId",
                table: "PrivateCoverage");

            migrationBuilder.RenameTable(
                name: "PrivateCoverage",
                newName: "PrivateCoverages");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateCoverage_PrivateCoverageOptionId",
                table: "PrivateCoverages",
                newName: "IX_PrivateCoverages_PrivateCoverageOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateCoverage_InsuranceCoverageId",
                table: "PrivateCoverages",
                newName: "IX_PrivateCoverages_InsuranceCoverageId");

            migrationBuilder.AddColumn<int>(
                name: "PrivateCoverageId",
                table: "InsuredPersons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "InsuranceCoverages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateCoverages",
                table: "PrivateCoverages",
                column: "PrivateCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCoverages_InsuranceId",
                table: "InsuranceCoverages",
                column: "InsuranceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                table: "InsuranceCoverages",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "InsuranceId",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverages_PrivateCoverageOption_PrivateCoverageOptionId",
                table: "PrivateCoverages",
                column: "PrivateCoverageOptionId",
                principalTable: "PrivateCoverageOption",
                principalColumn: "PrivateCoverageOptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceCoverages_Insurances_InsuranceId",
                table: "InsuranceCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_PrivateCoverageOption_PrivateCoverageOptionId",
                table: "PrivateCoverages");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceCoverages_InsuranceId",
                table: "InsuranceCoverages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateCoverages",
                table: "PrivateCoverages");

            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.DropColumn(
                name: "PrivateCoverageId",
                table: "InsuredPersons");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "InsuranceCoverages");

            migrationBuilder.RenameTable(
                name: "PrivateCoverages",
                newName: "PrivateCoverage");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateCoverages_PrivateCoverageOptionId",
                table: "PrivateCoverage",
                newName: "IX_PrivateCoverage_PrivateCoverageOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateCoverages_InsuranceCoverageId",
                table: "PrivateCoverage",
                newName: "IX_PrivateCoverage_InsuranceCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateCoverage",
                table: "PrivateCoverage",
                column: "PrivateCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_InsuranceCoverageId",
                table: "Insurances",
                column: "InsuranceCoverageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverage_InsuredPersonId",
                table: "PrivateCoverage",
                column: "InsuredPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_InsuranceCoverages_InsuranceCoverageId",
                table: "Insurances",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "PrivateCoverage",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverage_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverage",
                column: "InsuredPersonId",
                principalTable: "InsuredPersons",
                principalColumn: "InsuredPersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverage_PrivateCoverageOption_PrivateCoverageOptionId",
                table: "PrivateCoverage",
                column: "PrivateCoverageOptionId",
                principalTable: "PrivateCoverageOption",
                principalColumn: "PrivateCoverageOptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
