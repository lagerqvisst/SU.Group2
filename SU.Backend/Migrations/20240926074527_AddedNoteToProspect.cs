using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class AddedNoteToProspect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNote",
                table: "Prospects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNote",
                table: "Prospects");
        }
    }
}
