using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Correcciones_Vale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UsuarioId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_PROV_ProductoProductoId",
                table: "PROV_DetalleVale");

            migrationBuilder.DropTable(
                name: "PROV_DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UsuarioId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                newName: "ProductoId");

            migrationBuilder.RenameColumn(
                name: "IVA",
                table: "PROV_DetalleVale",
                newName: "Importe");

            migrationBuilder.RenameIndex(
                name: "IX_PROV_DetalleVale_PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                newName: "IX_PROV_DetalleVale_ProductoId");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_ProductoId",
                table: "PROV_DetalleVale",
                column: "ProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_ProductoId",
                table: "PROV_DetalleVale");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "PROV_DetalleVale",
                newName: "PROV_ProductoProductoId");

            migrationBuilder.RenameColumn(
                name: "Importe",
                table: "PROV_DetalleVale",
                newName: "IVA");

            migrationBuilder.RenameIndex(
                name: "IX_PROV_DetalleVale_ProductoId",
                table: "PROV_DetalleVale",
                newName: "IX_PROV_DetalleVale_PROV_ProductoProductoId");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PROV_DetalleProducto",
                columns: table => new
                {
                    DetalleProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DetalleValeId = table.Column<int>(type: "int", nullable: true),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Precio = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_DetalleProducto", x => x.DetalleProductoId);
                    table.ForeignKey(
                        name: "FK_PROV_DetalleProducto_PROV_DetalleVale_DetalleValeId",
                        column: x => x.DetalleValeId,
                        principalTable: "PROV_DetalleVale",
                        principalColumn: "DetalleValeId");
                    table.ForeignKey(
                        name: "FK_PROV_DetalleProducto_PROV_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "PROV_Producto",
                        principalColumn: "ProductoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UsuarioId",
                table: "AspNetUsers",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleProducto_DetalleValeId",
                table: "PROV_DetalleProducto",
                column: "DetalleValeId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleProducto_ProductoId",
                table: "PROV_DetalleProducto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UsuarioId",
                table: "AspNetUsers",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                column: "PROV_ProductoProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");
        }
    }
}
