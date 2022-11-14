using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class DetalleValemodificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleVale_ProductoId",
                table: "PROV_DetalleVale",
                column: "ProductoId");

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

            migrationBuilder.DropIndex(
                name: "IX_PROV_DetalleVale_ProductoId",
                table: "PROV_DetalleVale");
        }
    }
}
