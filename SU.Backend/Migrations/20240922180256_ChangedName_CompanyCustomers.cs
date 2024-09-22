using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class ChangedName_CompanyCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                table: "InsurancePolicyHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_CompanyCustomer_CompanyCustomerId",
                table: "Prospects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCustomer",
                table: "CompanyCustomer");

            migrationBuilder.RenameTable(
                name: "CompanyCustomer",
                newName: "CompanyCustomers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCustomers",
                table: "CompanyCustomers",
                column: "CompanyCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomers_CompanyCustomerId",
                table: "InsurancePolicyHolders",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomers",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_CompanyCustomers_CompanyCustomerId",
                table: "Prospects",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomers",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomers_CompanyCustomerId",
                table: "InsurancePolicyHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_CompanyCustomers_CompanyCustomerId",
                table: "Prospects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCustomers",
                table: "CompanyCustomers");

            migrationBuilder.RenameTable(
                name: "CompanyCustomers",
                newName: "CompanyCustomer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCustomer",
                table: "CompanyCustomer",
                column: "CompanyCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolicyHolders_CompanyCustomer_CompanyCustomerId",
                table: "InsurancePolicyHolders",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomer",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_CompanyCustomer_CompanyCustomerId",
                table: "Prospects",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomer",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
