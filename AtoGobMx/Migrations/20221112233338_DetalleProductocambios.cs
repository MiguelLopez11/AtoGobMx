using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class DetalleProductocambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleProducto_DetalleValeId",
                table: "PROV_DetalleProducto",
                column: "DetalleValeId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleProducto_ProductoId",
                table: "PROV_DetalleProducto",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleProducto_PROV_DetalleVale_DetalleValeId",
                table: "PROV_DetalleProducto",
                column: "DetalleValeId",
                principalTable: "PROV_DetalleVale",
                principalColumn: "DetalleValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleProducto_PROV_Producto_ProductoId",
                table: "PROV_DetalleProducto",
                column: "ProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleProducto_PROV_DetalleVale_DetalleValeId",
                table: "PROV_DetalleProducto");

            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleProducto_PROV_Producto_ProductoId",
                table: "PROV_DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_PROV_DetalleProducto_DetalleValeId",
                table: "PROV_DetalleProducto");

            migrationBuilder.DropIndex(
                name: "IX_PROV_DetalleProducto_ProductoId",
                table: "PROV_DetalleProducto");
        }
    }
}
