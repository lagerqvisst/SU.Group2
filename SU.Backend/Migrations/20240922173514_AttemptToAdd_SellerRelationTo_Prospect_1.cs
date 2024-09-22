using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class AttemptToAdd_SellerRelationTo_Prospect_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedAgentNumber",
                table: "Prospects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContactDate",
                table: "Prospects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Prospects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prospects_EmployeeId",
                table: "Prospects",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prospects_Employees_EmployeeId",
                table: "Prospects",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prospects_Employees_EmployeeId",
                table: "Prospects");

            migrationBuilder.DropIndex(
                name: "IX_Prospects_EmployeeId",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "AssignedAgentNumber",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "ContactDate",
                table: "Prospects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Prospects");
        }
    }
}
