using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ControlDeVales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlDeVales",
                columns: table => new
                {
                    ControlValeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaEmicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaVigencia = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    TipoCombustible = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    DetalleValeId = table.Column<float>(type: "float", nullable: true),
                    EstatusValeId = table.Column<int>(type: "int", nullable: true),
                    TipoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlDeVales", x => x.ControlValeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlDeVales");
        }
    }
}
