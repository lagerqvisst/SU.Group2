using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class ProspectUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RiskzoneLevel",
                table: "Riskzones",
                newName: "RiskZoneLevel");

            migrationBuilder.RenameColumn(
                name: "RiskzoneId",
                table: "Riskzones",
                newName: "RiskZoneId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Employees",
                newName: "UserName");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå7407H", "19700518-8119" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla3027L", "19851103-6946" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa9089P", "19821014-1318" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo3263J", "19810205-7578" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu8162S", "197302014-7608" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe7612P", "198406011-2473" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr4528F", "19930106-2184" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal9788A", "19750413-9575" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo1116J", "19850911-9442" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani3864N", "19860307-1078" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu5828L", "19870717-6776" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe8417H", "19880704-8991" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RiskZoneLevel",
                table: "Riskzones",
                newName: "RiskzoneLevel");

            migrationBuilder.RenameColumn(
                name: "RiskZoneId",
                table: "Riskzones",
                newName: "RiskzoneId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Employees",
                newName: "Username");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå3268H", "19700518-1433" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla5298L", "19851103-6358" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa7928P", "19821014-5936" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo2163J", "19810205-4352" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu7019S", "197302014-7684" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe5886P", "198406011-7079" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr6932F", "19930106-7288" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal7260A", "19750413-9523" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo4934J", "19850911-3008" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani5303N", "19860307-6870" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu2921L", "19870717-3476" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe7117H", "19880704-5299" });
        }
    }
}
