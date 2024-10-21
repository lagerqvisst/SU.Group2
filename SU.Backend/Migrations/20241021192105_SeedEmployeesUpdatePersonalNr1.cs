using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class SeedEmployeesUpdatePersonalNr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå8356H", "19700101-2549" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla9569L", "19850101-5186" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa6788P", "19820101-8808" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo4205J", "19810101-9874" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu1966S", "197302014-5744" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe8191P", "198406011-6061" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr6909F", "19930106-3506" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal6075A", "19750101-3021" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo4344J", "19850101-1284" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani9142N", "19860101-1534" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu4806L", "19870101-5376" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe9666H", "19880101-5063" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå8963H", "19700101-3061" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla1884L", "19850101-1852" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa7758P", "19820101-5628" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo6781J", "19810101-7252" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu9341S", "197302014-1768" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe2035P", "198406011-2561" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr4016F", "19930106-8138" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal8789A", "19750101-5357" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo9871J", "19850101-7784" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani2196N", "19860101-2842" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu1406L", "19870101-7708" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe6340H", "19880101-6979" });
        }
    }
}
