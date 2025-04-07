using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARKE_Landing_Page.Migrations
{
    /// <inheritdoc />
    public partial class SerialNumberPropertyAddedToMachineEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Machines",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Machines");
        }
    }
}
