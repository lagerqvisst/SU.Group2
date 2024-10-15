using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class FixForInsuranceCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsurancePolicyHolderId",
                table: "Insurances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsurancePolicyHolderId",
                table: "Insurances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
