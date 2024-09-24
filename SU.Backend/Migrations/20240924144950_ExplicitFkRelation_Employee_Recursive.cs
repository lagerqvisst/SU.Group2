using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class ExplicitFkRelation_Employee_Recursive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerEmployeeId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ManagerEmployeeId",
                table: "Employees",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ManagerEmployeeId",
                table: "Employees",
                newName: "IX_Employees_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Employees",
                newName: "ManagerEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                newName: "IX_Employees_ManagerEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerEmployeeId",
                table: "Employees",
                column: "ManagerEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
