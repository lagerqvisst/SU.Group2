using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Seller_Prospect_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_Employees_EmployeeId",
                table: "Prospects");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Prospects",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Prospects_EmployeeId",
                table: "Prospects",
                newName: "IX_Prospects_SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_Employees_SellerId",
                table: "Prospects",
                column: "SellerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_Employees_SellerId",
                table: "Prospects");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Prospects",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Prospects_SellerId",
                table: "Prospects",
                newName: "IX_Prospects_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_Employees_EmployeeId",
                table: "Prospects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
