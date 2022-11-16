using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class SERVICIOS_MEDICOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaMedicamentoId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SERMED_ProductosReceta",
                columns: table => new
                {
                    ProductoRecetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RecetaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SERMED_ProductoProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SERMED_ProductosReceta", x => x.ProductoRecetaId);
                    table.ForeignKey(
                        name: "FK_SERMED_ProductosReceta_Medicamentos_SERMED_ProductoProductoId",
                        column: x => x.SERMED_ProductoProductoId,
                        principalTable: "Medicamentos",
                        principalColumn: "ProductoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_SERMED_ProductosReceta_SERMED_ProductoProductoId",
                table: "SERMED_ProductosReceta",
                column: "SERMED_ProductoProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SERMED_ProductosReceta");

            migrationBuilder.DropColumn(
                name: "CategoriaMedicamentoId",
                table: "Medicamentos");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
