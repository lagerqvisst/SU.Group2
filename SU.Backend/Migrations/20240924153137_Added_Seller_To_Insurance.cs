using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Added_Seller_To_Insurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Insurances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Insurances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_EmployeeId",
                table: "Insurances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_SellerId",
                table: "Insurances",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Employees_EmployeeId",
                table: "Insurances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Employees_SellerId",
                table: "Insurances",
                column: "SellerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Employees_EmployeeId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Employees_SellerId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_EmployeeId",
                table: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Insurances_SellerId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Insurances");
        }
    }
}
