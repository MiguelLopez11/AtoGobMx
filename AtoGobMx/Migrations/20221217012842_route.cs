using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class route : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "Origen",
                table: "Ruta");

            migrationBuilder.CreateTable(
                name: "Coordenadas_Ruta",
                columns: table => new
                {
                    CoordenadaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Latitud = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RutaId = table.Column<int>(type: "int", nullable: false),
                    OrdenCoordenada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordenadas_Ruta", x => x.CoordenadaId);
                    table.ForeignKey(
                        name: "FK_Coordenadas_Ruta_Ruta_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Ruta",
                        principalColumn: "RutaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Coordenadas_Ruta_RutaId",
                table: "Coordenadas_Ruta",
                column: "RutaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coordenadas_Ruta");

            migrationBuilder.AddColumn<string>(
                name: "Destino",
                table: "Ruta",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Origen",
                table: "Ruta",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
