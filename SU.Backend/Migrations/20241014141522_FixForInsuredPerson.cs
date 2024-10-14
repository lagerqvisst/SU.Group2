using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class FixForInsuredPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.DropTable(
                name: "InsuredPersons");

            migrationBuilder.DropIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.DropColumn(
                name: "InsuredPersonId",
                table: "PrivateCoverages");

            migrationBuilder.AddColumn<string>(
                name: "InsuredPersonName",
                table: "PrivateCoverages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuredPersonPersonalNumber",
                table: "PrivateCoverages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuredPersonName",
                table: "PrivateCoverages");

            migrationBuilder.DropColumn(
                name: "InsuredPersonPersonalNumber",
                table: "PrivateCoverages");

            migrationBuilder.AddColumn<int>(
                name: "InsuredPersonId",
                table: "PrivateCoverages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InsuredPersons",
                columns: table => new
                {
                    InsuredPersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuredPersons", x => x.InsuredPersonId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateCoverages_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateCoverages_InsuredPersons_InsuredPersonId",
                table: "PrivateCoverages",
                column: "InsuredPersonId",
                principalTable: "InsuredPersons",
                principalColumn: "InsuredPersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
