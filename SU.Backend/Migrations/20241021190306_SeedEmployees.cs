using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class SeedEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "LiabilityCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityCoverage_LiabilityCoverageOption_LiabilityCoverageOptionId",
                table: "LiabilityCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAndInventoryCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverage_Riskzones_RiskzoneId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleInsuranceCoverage",
                table: "VehicleInsuranceCoverage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyAndInventoryCoverage",
                table: "PropertyAndInventoryCoverage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiabilityCoverage",
                table: "LiabilityCoverage");

            migrationBuilder.RenameTable(
                name: "VehicleInsuranceCoverage",
                newName: "VehicleInsuranceCoverages");

            migrationBuilder.RenameTable(
                name: "PropertyAndInventoryCoverage",
                newName: "PropertyAndInventoryCoverages");

            migrationBuilder.RenameTable(
                name: "LiabilityCoverage",
                newName: "LiabilityCoverages");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleInsuranceCoverage_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverages",
                newName: "IX_VehicleInsuranceCoverages_VehicleInsuranceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleInsuranceCoverage_RiskzoneId",
                table: "VehicleInsuranceCoverages",
                newName: "IX_VehicleInsuranceCoverages_RiskzoneId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleInsuranceCoverage_InsuranceCoverageId",
                table: "VehicleInsuranceCoverages",
                newName: "IX_VehicleInsuranceCoverages_InsuranceCoverageId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyAndInventoryCoverage_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverages",
                newName: "IX_PropertyAndInventoryCoverages_InsuranceCoverageId");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityCoverage_LiabilityCoverageOptionId",
                table: "LiabilityCoverages",
                newName: "IX_LiabilityCoverages_LiabilityCoverageOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityCoverage_InsuranceCoverageId",
                table: "LiabilityCoverages",
                newName: "IX_LiabilityCoverages_InsuranceCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleInsuranceCoverages",
                table: "VehicleInsuranceCoverages",
                column: "VehicleInsuranceCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyAndInventoryCoverages",
                table: "PropertyAndInventoryCoverages",
                column: "PropertyAndInventoryCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiabilityCoverages",
                table: "LiabilityCoverages",
                column: "LiabilityCoverageId");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AgentNumber", "BaseSalary", "Email", "FirstName", "LastName", "ManagerId", "Password", "PersonalNumber", "Username" },
                values: new object[] { 1, null, 100000, "sten.hård@toppinsurance.se", "Sten", "Hård", null, "sthå2175H", "19700101-0000", "sthå" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AgentNumber", "BaseSalary", "Email", "FirstName", "LastName", "ManagerId", "Password", "PersonalNumber", "Username" },
                values: new object[] { 2, null, 32000, "ann-sofie.larsson@toppinsurance.se", "Ann-Sofie", "Larsson", 1, "anla7523L", "19850101-0000", "anla" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AgentNumber", "BaseSalary", "Email", "FirstName", "LastName", "ManagerId", "Password", "PersonalNumber", "Username" },
                values: new object[] { 3, null, 50000, "iren.panik@toppinsurance.se", "Iren", "Panik", 1, "irpa6673P", "19820101-0000", "irpa" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AgentNumber", "BaseSalary", "Email", "FirstName", "LastName", "ManagerId", "Password", "PersonalNumber", "Username" },
                values: new object[,]
                {
                    { 4, "2547", 24000, "irene.johansson@toppinsurance.se", "Irene", "Johansson", 3, "irjo9337J", "19810101-0000", "irjo" },
                    { 5, "6423", 28000, "karin.sundberg@toppinsurance.se", "Karin", "Sundberg", 3, "kasu4654S", "19890101-0000", "kasu" },
                    { 6, "2447", 24000, "vigo.persson@toppinsurance.se", "Vigo", "Persson", 3, "vipe3496P", "19900101-0000", "vipe" },
                    { 7, "5836", 24000, "birgitta.frisk@toppinsurance.se", "Birgitta", "Frisk", 3, "bifr7809F", "19830101-0000", "bifr" },
                    { 8, "2264", 24000, "boris.alm@toppinsurance.se", "Boris", "Alm", 3, "boal5136A", "19750101-0000", "boal" },
                    { 9, "1153", 24000, "linda.jonsson@toppinsurance.se", "Linda", "Jonsson", 3, "lijo7653J", "19850101-0000", "lijo" },
                    { 10, "7473", 24000, "malin.nilsdotter@toppinsurance.se", "Malin", "Nilsdotter", 3, "mani6771N", "19860101-0000", "mani" },
                    { 11, "4331", 24000, "mikael.lund@toppinsurance.se", "Mikael", "Lund", 3, "milu5491L", "19870101-0000", "milu" },
                    { 12, "7337", 24000, "patrik.hedman@toppinsurance.se", "Patrik", "Hedman", 3, "pahe6588H", "19880101-0000", "pahe" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "LiabilityCoverages",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityCoverages_LiabilityCoverageOption_LiabilityCoverageOptionId",
                table: "LiabilityCoverages",
                column: "LiabilityCoverageOptionId",
                principalTable: "LiabilityCoverageOption",
                principalColumn: "LiabilityCoverageOptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAndInventoryCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverages",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "VehicleInsuranceCoverages",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverages_Riskzones_RiskzoneId",
                table: "VehicleInsuranceCoverages",
                column: "RiskzoneId",
                principalTable: "Riskzones",
                principalColumn: "RiskzoneId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverages_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverages",
                column: "VehicleInsuranceOptionId",
                principalTable: "VehicleInsuranceOptions",
                principalColumn: "VehicleInsuranceOptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "LiabilityCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_LiabilityCoverages_LiabilityCoverageOption_LiabilityCoverageOptionId",
                table: "LiabilityCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyAndInventoryCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverages_InsuranceCoverages_InsuranceCoverageId",
                table: "VehicleInsuranceCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverages_Riskzones_RiskzoneId",
                table: "VehicleInsuranceCoverages");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleInsuranceCoverages_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleInsuranceCoverages",
                table: "VehicleInsuranceCoverages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyAndInventoryCoverages",
                table: "PropertyAndInventoryCoverages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LiabilityCoverages",
                table: "LiabilityCoverages");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "VehicleInsuranceCoverages",
                newName: "VehicleInsuranceCoverage");

            migrationBuilder.RenameTable(
                name: "PropertyAndInventoryCoverages",
                newName: "PropertyAndInventoryCoverage");

            migrationBuilder.RenameTable(
                name: "LiabilityCoverages",
                newName: "LiabilityCoverage");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleInsuranceCoverages_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage",
                newName: "IX_VehicleInsuranceCoverage_VehicleInsuranceOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleInsuranceCoverages_RiskzoneId",
                table: "VehicleInsuranceCoverage",
                newName: "IX_VehicleInsuranceCoverage_RiskzoneId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleInsuranceCoverages_InsuranceCoverageId",
                table: "VehicleInsuranceCoverage",
                newName: "IX_VehicleInsuranceCoverage_InsuranceCoverageId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyAndInventoryCoverages_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverage",
                newName: "IX_PropertyAndInventoryCoverage_InsuranceCoverageId");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityCoverages_LiabilityCoverageOptionId",
                table: "LiabilityCoverage",
                newName: "IX_LiabilityCoverage_LiabilityCoverageOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_LiabilityCoverages_InsuranceCoverageId",
                table: "LiabilityCoverage",
                newName: "IX_LiabilityCoverage_InsuranceCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleInsuranceCoverage",
                table: "VehicleInsuranceCoverage",
                column: "VehicleInsuranceCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyAndInventoryCoverage",
                table: "PropertyAndInventoryCoverage",
                column: "PropertyAndInventoryCoverageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LiabilityCoverage",
                table: "LiabilityCoverage",
                column: "LiabilityCoverageId");

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "LiabilityCoverage",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LiabilityCoverage_LiabilityCoverageOption_LiabilityCoverageOptionId",
                table: "LiabilityCoverage",
                column: "LiabilityCoverageOptionId",
                principalTable: "LiabilityCoverageOption",
                principalColumn: "LiabilityCoverageOptionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyAndInventoryCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "PropertyAndInventoryCoverage",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverage_InsuranceCoverages_InsuranceCoverageId",
                table: "VehicleInsuranceCoverage",
                column: "InsuranceCoverageId",
                principalTable: "InsuranceCoverages",
                principalColumn: "InsuranceCoverageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverage_Riskzones_RiskzoneId",
                table: "VehicleInsuranceCoverage",
                column: "RiskzoneId",
                principalTable: "Riskzones",
                principalColumn: "RiskzoneId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleInsuranceCoverage_VehicleInsuranceOptions_VehicleInsuranceOptionId",
                table: "VehicleInsuranceCoverage",
                column: "VehicleInsuranceOptionId",
                principalTable: "VehicleInsuranceOptions",
                principalColumn: "VehicleInsuranceOptionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
