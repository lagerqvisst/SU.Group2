using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class RefactorInsuranceAddon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraPremium",
                table: "InsuranceAddons");

            migrationBuilder.DropColumn(
                name: "InsuranceAddonType",
                table: "InsuranceAddons");

            migrationBuilder.DropColumn(
                name: "LongTermSicknessOption",
                table: "InsuranceAddons");

            migrationBuilder.DropColumn(
                name: "SicknessAccidentOption",
                table: "InsuranceAddons");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceAddonTypeId",
                table: "InsuranceAddons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InsuranceAddonTypes",
                columns: table => new
                {
                    InsuranceAddonTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseExtraPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceAddonTypes", x => x.InsuranceAddonTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceAddons_InsuranceAddonTypeId",
                table: "InsuranceAddons",
                column: "InsuranceAddonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceAddons_InsuranceAddonTypes_InsuranceAddonTypeId",
                table: "InsuranceAddons",
                column: "InsuranceAddonTypeId",
                principalTable: "InsuranceAddonTypes",
                principalColumn: "InsuranceAddonTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceAddons_InsuranceAddonTypes_InsuranceAddonTypeId",
                table: "InsuranceAddons");

            migrationBuilder.DropTable(
                name: "InsuranceAddonTypes");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceAddons_InsuranceAddonTypeId",
                table: "InsuranceAddons");

            migrationBuilder.DropColumn(
                name: "InsuranceAddonTypeId",
                table: "InsuranceAddons");

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraPremium",
                table: "InsuranceAddons",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceAddonType",
                table: "InsuranceAddons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LongTermSicknessOption",
                table: "InsuranceAddons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SicknessAccidentOption",
                table: "InsuranceAddons",
                type: "int",
                nullable: true);
        }
    }
}
