using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class SeedEmployeeRoleAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmployeeRoleAssignments",
                columns: new[] { "EmployeeRoleAssignmentId", "EmployeeId", "Percentage", "Role" },
                values: new object[,]
                {
                    { 1, 1, 100.0, "CEO" },
                    { 2, 2, 100.0, "FinancialAssistant" },
                    { 3, 3, 100.0, "SalesManager" },
                    { 4, 4, 100.0, "InsideSales" },
                    { 5, 5, 75.0, "InsideSales" },
                    { 6, 5, 25.0, "SalesAssistant" },
                    { 7, 6, 100.0, "InsideSales" },
                    { 8, 7, 100.0, "OutsideSales" },
                    { 9, 8, 100.0, "OutsideSales" },
                    { 10, 9, 100.0, "OutsideSales" },
                    { 11, 10, 100.0, "OutsideSales" },
                    { 12, 11, 100.0, "OutsideSales" },
                    { 13, 12, 100.0, "OutsideSales" }
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "sthå7408H");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "Password",
                value: "anla2271L");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                column: "Password",
                value: "irpa8921P");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                column: "Password",
                value: "irjo3691J");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                column: "Password",
                value: "kasu4252S");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                column: "Password",
                value: "vipe1453P");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                column: "Password",
                value: "bifr8367F");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                column: "Password",
                value: "boal1035A");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                column: "Password",
                value: "lijo2572J");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                column: "Password",
                value: "mani1449N");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                column: "Password",
                value: "milu5596L");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                column: "Password",
                value: "pahe3183H");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "EmployeeRoleAssignments",
                keyColumn: "EmployeeRoleAssignmentId",
                keyValue: 13);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Password",
                value: "sthå2175H");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "Password",
                value: "anla7523L");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                column: "Password",
                value: "irpa6673P");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                column: "Password",
                value: "irjo9337J");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                column: "Password",
                value: "kasu4654S");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                column: "Password",
                value: "vipe3496P");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                column: "Password",
                value: "bifr7809F");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                column: "Password",
                value: "boal5136A");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                column: "Password",
                value: "lijo7653J");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                column: "Password",
                value: "mani6771N");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                column: "Password",
                value: "milu5491L");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                column: "Password",
                value: "pahe6588H");
        }
    }
}
