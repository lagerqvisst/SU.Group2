using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class MinorFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleAssignment_Employees_EmployeeId",
                table: "EmployeeRoleAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoleAssignment",
                table: "EmployeeRoleAssignment");

            migrationBuilder.RenameTable(
                name: "EmployeeRoleAssignment",
                newName: "EmployeeRoleAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoleAssignment_EmployeeId",
                table: "EmployeeRoleAssignments",
                newName: "IX_EmployeeRoleAssignments_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoleAssignments",
                table: "EmployeeRoleAssignments",
                column: "EmployeeRoleAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleAssignments_Employees_EmployeeId",
                table: "EmployeeRoleAssignments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoleAssignments_Employees_EmployeeId",
                table: "EmployeeRoleAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRoleAssignments",
                table: "EmployeeRoleAssignments");

            migrationBuilder.RenameTable(
                name: "EmployeeRoleAssignments",
                newName: "EmployeeRoleAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoleAssignments_EmployeeId",
                table: "EmployeeRoleAssignment",
                newName: "IX_EmployeeRoleAssignment_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRoleAssignment",
                table: "EmployeeRoleAssignment",
                column: "EmployeeRoleAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoleAssignment_Employees_EmployeeId",
                table: "EmployeeRoleAssignment",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
