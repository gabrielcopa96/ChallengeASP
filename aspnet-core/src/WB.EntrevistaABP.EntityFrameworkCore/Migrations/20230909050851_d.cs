using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WB.EntrevistaABP.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleViajes");

            migrationBuilder.DropTable(
                name: "Viajes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viajes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Destino = table.Column<string>(type: "text", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    FechaLlegada = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    MedioTransporte = table.Column<string>(type: "text", nullable: false),
                    Origen = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viajes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetalleViajes",
                columns: table => new
                {
                    ViajeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PasajeroId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleViajes", x => new { x.ViajeId, x.PasajeroId });
                    table.ForeignKey(
                        name: "FK_DetalleViajes_Pasajeros_PasajeroId",
                        column: x => x.PasajeroId,
                        principalTable: "Pasajeros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleViajes_Viajes_ViajeId",
                        column: x => x.ViajeId,
                        principalTable: "Viajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleViajes_PasajeroId",
                table: "DetalleViajes",
                column: "PasajeroId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleViajes_ViajeId_PasajeroId",
                table: "DetalleViajes",
                columns: new[] { "ViajeId", "PasajeroId" },
                unique: true);
        }
    }
}
