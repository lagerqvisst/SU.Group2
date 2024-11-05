using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class RemoveProspects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prospects");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prospects",
                columns: table => new
                {
                    ProspectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCustomerId = table.Column<int>(type: "int", nullable: true),
                    PrivateCustomerId = table.Column<int>(type: "int", nullable: true),
                    SellerId = table.Column<int>(type: "int", nullable: true),
                    AssignedAgentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProspectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProspectType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospects", x => x.ProspectId);
                    table.ForeignKey(
                        name: "FK_Prospects_CompanyCustomers_CompanyCustomerId",
                        column: x => x.CompanyCustomerId,
                        principalTable: "CompanyCustomers",
                        principalColumn: "CompanyCustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_Employees_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospects_PrivateCustomers_PrivateCustomerId",
                        column: x => x.PrivateCustomerId,
                        principalTable: "PrivateCustomers",
                        principalColumn: "PrivateCustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_CompanyCustomerId",
                table: "Prospects",
                column: "CompanyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_PrivateCustomerId",
                table: "Prospects",
                column: "PrivateCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_SellerId",
                table: "Prospects",
                column: "SellerId");
        }
    }
}
