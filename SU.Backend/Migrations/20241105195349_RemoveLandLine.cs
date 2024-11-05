using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class RemoveLandLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyLandlineNumber",
                table: "CompanyCustomers");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå3586H", "19700518-1749" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla3155L", "19851103-6936" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa8714P", "19821014-2932" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo5153J", "19810205-5640" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu4982S", "197302014-2594" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe2153P", "198406011-1703" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr5251F", "19930106-9878" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal1774A", "19750413-8769" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo5652J", "19850911-5720" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani2066N", "19860307-7774" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu5392L", "19870717-1802" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe5821H", "19880704-8153" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyLandlineNumber",
                table: "CompanyCustomers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "sthå3535H", "19700518-6265" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "anla6581L", "19851103-5778" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irpa5582P", "19821014-8392" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "irjo5476J", "19810205-8204" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "kasu9645S", "197302014-1468" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "vipe2977P", "198406011-3457" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "bifr2477F", "19930106-7058" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "boal9416A", "19750413-5707" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "lijo3910J", "19850911-2950" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "mani1212N", "19860307-3710" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "milu6276L", "19870717-4066" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12,
                columns: new[] { "Password", "PersonalNumber" },
                values: new object[] { "pahe6390H", "19880704-4119" });
        }
    }
}
