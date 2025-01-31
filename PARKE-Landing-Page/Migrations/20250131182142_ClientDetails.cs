using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARKE_Landing_Page.Migrations
{
    /// <inheritdoc />
    public partial class ClientDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientDetails_Machines_MachineId",
                table: "ClientDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Clients_ClientId",
                table: "Machines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_ClientId",
                table: "Machines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientDetails",
                table: "ClientDetails");

            migrationBuilder.DropIndex(
                name: "IX_ClientDetails_ClientId",
                table: "ClientDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClientDetails");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "ClientDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientDetails",
                table: "ClientDetails",
                columns: new[] { "ClientId", "MachineId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientDetails_Machines_MachineId",
                table: "ClientDetails",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientDetails_Machines_MachineId",
                table: "ClientDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientDetails",
                table: "ClientDetails");

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "ClientDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClientDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientDetails",
                table: "ClientDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_ClientId",
                table: "Machines",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetails_ClientId",
                table: "ClientDetails",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientDetails_Machines_MachineId",
                table: "ClientDetails",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Clients_ClientId",
                table: "Machines",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
