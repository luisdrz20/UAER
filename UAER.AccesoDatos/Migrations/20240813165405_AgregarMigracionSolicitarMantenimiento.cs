using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UAER.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarMigracionSolicitarMantenimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitarMantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSolicitante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    FechaSolicitud = table.Column<DateTime>(type: "date", nullable: false),
                    FechaAsignadaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaAsignadaFinal = table.Column<DateTime>(type: "date", nullable: false),
                    AreasSId = table.Column<int>(type: "int", nullable: false),
                    MantenimientoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitarMantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitarMantenimientos_AreasS_AreasSId",
                        column: x => x.AreasSId,
                        principalTable: "AreasS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitarMantenimientos_Mantenimientos_MantenimientoId",
                        column: x => x.MantenimientoId,
                        principalTable: "Mantenimientos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitarMantenimientos_AreasSId",
                table: "SolicitarMantenimientos",
                column: "AreasSId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitarMantenimientos_MantenimientoId",
                table: "SolicitarMantenimientos",
                column: "MantenimientoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitarMantenimientos");
        }
    }
}
