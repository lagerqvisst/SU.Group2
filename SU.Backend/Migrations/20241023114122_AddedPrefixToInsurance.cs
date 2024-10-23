using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class AddedPrefixToInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InsuranceCategory",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceCategory",
                table: "Insurances");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå8310H", "19700518-9117" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla1067L", "19851103-1650" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa4972P", "19821014-8598" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo5364J", "19810205-8546" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu2891S", "197302014-8094" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe6938P", "198406011-9665" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr6063F", "19930106-2952" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal2944A", "19750413-2503" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo4077J", "19850911-6090" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani6243N", "19860307-4876" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu3240L", "19870717-7290" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe5740H", "19880704-3961" });
        }
    }
}
