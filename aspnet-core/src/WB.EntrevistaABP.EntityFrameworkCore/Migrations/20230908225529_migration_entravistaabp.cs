using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class migrationentravistaabp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Pasajeros",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pasajeros_UserId",
                table: "Pasajeros",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pasajeros_AbpUsers_UserId",
                table: "Pasajeros",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pasajeros_AbpUsers_UserId",
                table: "Pasajeros");

            migrationBuilder.DropIndex(
                name: "IX_Pasajeros_UserId",
                table: "Pasajeros");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pasajeros");
        }
    }
}
