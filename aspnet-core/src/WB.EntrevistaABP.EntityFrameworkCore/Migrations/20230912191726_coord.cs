using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class coord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coordinador",
                table: "Viajes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinador",
                table: "Viajes");
        }
    }
}
