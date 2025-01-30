using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARKE_Landing_Page.Migrations
{
    /// <inheritdoc />
    public partial class ClientLoginCredentialsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_ClientId",
                table: "Machines",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Clients_ClientId",
                table: "Machines",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Clients_ClientId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_ClientId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Clients");
        }
    }
}
