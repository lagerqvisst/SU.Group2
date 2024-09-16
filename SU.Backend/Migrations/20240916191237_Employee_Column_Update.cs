using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Employee_Column_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "PrivateCustomer");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "PrivateCustomer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonalNumber",
                table: "PrivateCustomer",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "PrivateCustomer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
