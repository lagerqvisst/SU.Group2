using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class UpdateToTableName_Prospects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospect_CompanyCustomer_CompanyCustomerId",
                table: "Prospect");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospect_PrivateCustomers_PrivateCustomerId",
                table: "Prospect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prospect",
                table: "Prospect");

            migrationBuilder.RenameTable(
                name: "Prospect",
                newName: "Prospects");

            migrationBuilder.RenameIndex(
                name: "IX_Prospect_PrivateCustomerId",
                table: "Prospects",
                newName: "IX_Prospects_PrivateCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Prospect_CompanyCustomerId",
                table: "Prospects",
                newName: "IX_Prospects_CompanyCustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Prospects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prospects",
                table: "Prospects",
                column: "ProspectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_CompanyCustomer_CompanyCustomerId",
                table: "Prospects",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomer",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_PrivateCustomers_PrivateCustomerId",
                table: "Prospects",
                column: "PrivateCustomerId",
                principalTable: "PrivateCustomers",
                principalColumn: "PrivateCustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_CompanyCustomer_CompanyCustomerId",
                table: "Prospects");

            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_PrivateCustomers_PrivateCustomerId",
                table: "Prospects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prospects",
                table: "Prospects");

            migrationBuilder.RenameTable(
                name: "Prospects",
                newName: "Prospect");

            migrationBuilder.RenameIndex(
                name: "IX_Prospects_PrivateCustomerId",
                table: "Prospect",
                newName: "IX_Prospect_PrivateCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Prospects_CompanyCustomerId",
                table: "Prospect",
                newName: "IX_Prospect_CompanyCustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Prospect",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prospect",
                table: "Prospect",
                column: "ProspectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospect_CompanyCustomer_CompanyCustomerId",
                table: "Prospect",
                column: "CompanyCustomerId",
                principalTable: "CompanyCustomer",
                principalColumn: "CompanyCustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prospect_PrivateCustomers_PrivateCustomerId",
                table: "Prospect",
                column: "PrivateCustomerId",
                principalTable: "PrivateCustomers",
                principalColumn: "PrivateCustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
