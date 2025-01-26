using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARKE_Landing_Page.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPrueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Machines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Clients");
        }
    }
}
