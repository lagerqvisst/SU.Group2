using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SU.Backend.Migrations
{
    public partial class Continued_Prospect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prospect",
                columns: table => new
                {
                    ProspectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PrivateCustomerId = table.Column<int>(type: "int", nullable: true),
                    CompanyCustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prospect", x => x.ProspectId);
                    table.ForeignKey(
                        name: "FK_Prospect_CompanyCustomer_CompanyCustomerId",
                        column: x => x.CompanyCustomerId,
                        principalTable: "CompanyCustomer",
                        principalColumn: "CompanyCustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prospect_PrivateCustomers_PrivateCustomerId",
                        column: x => x.PrivateCustomerId,
                        principalTable: "PrivateCustomers",
                        principalColumn: "PrivateCustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prospect_CompanyCustomerId",
                table: "Prospect",
                column: "CompanyCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prospect_PrivateCustomerId",
                table: "Prospect",
                column: "PrivateCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prospect");
        }
    }
}
