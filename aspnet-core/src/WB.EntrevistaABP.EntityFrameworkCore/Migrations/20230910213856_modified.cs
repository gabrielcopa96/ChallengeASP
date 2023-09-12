using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class modified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pasajeros_AbpUsers_UserId",
                table: "Pasajeros");

            migrationBuilder.AddForeignKey(
                name: "FK_Pasajeros_AbpUsers_UserId",
                table: "Pasajeros",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pasajeros_AbpUsers_UserId",
                table: "Pasajeros");

            migrationBuilder.AddForeignKey(
                name: "FK_Pasajeros_AbpUsers_UserId",
                table: "Pasajeros",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
