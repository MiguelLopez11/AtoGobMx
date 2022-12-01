using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_vales2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_PROV_DetalleVale_DetalleValeId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_PROV_Producto_ProductoId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_ControlDeVales_PROV_ControlValesControlVale~",
                table: "PROV_DetalleVale");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_DetalleValeId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_ProductoId",
                table: "ControlDeVales");

            migrationBuilder.DropColumn(
                name: "DetalleValeId",
                table: "ControlDeVales");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "ControlDeVales");

            migrationBuilder.RenameColumn(
                name: "PROV_ControlValesControlValeId",
                table: "PROV_DetalleVale",
                newName: "ControlValeId");

            migrationBuilder.RenameIndex(
                name: "IX_PROV_DetalleVale_PROV_ControlValesControlValeId",
                table: "PROV_DetalleVale",
                newName: "IX_PROV_DetalleVale_ControlValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_ControlDeVales_ControlValeId",
                table: "PROV_DetalleVale",
                column: "ControlValeId",
                principalTable: "ControlDeVales",
                principalColumn: "ControlValeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_ControlDeVales_ControlValeId",
                table: "PROV_DetalleVale");

            migrationBuilder.RenameColumn(
                name: "ControlValeId",
                table: "PROV_DetalleVale",
                newName: "PROV_ControlValesControlValeId");

            migrationBuilder.RenameIndex(
                name: "IX_PROV_DetalleVale_ControlValeId",
                table: "PROV_DetalleVale",
                newName: "IX_PROV_DetalleVale_PROV_ControlValesControlValeId");

            migrationBuilder.AddColumn<int>(
                name: "DetalleValeId",
                table: "ControlDeVales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "ControlDeVales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_DetalleValeId",
                table: "ControlDeVales",
                column: "DetalleValeId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_ProductoId",
                table: "ControlDeVales",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_PROV_DetalleVale_DetalleValeId",
                table: "ControlDeVales",
                column: "DetalleValeId",
                principalTable: "PROV_DetalleVale",
                principalColumn: "DetalleValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_PROV_Producto_ProductoId",
                table: "ControlDeVales",
                column: "ProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_ControlDeVales_PROV_ControlValesControlVale~",
                table: "PROV_DetalleVale",
                column: "PROV_ControlValesControlValeId",
                principalTable: "ControlDeVales",
                principalColumn: "ControlValeId");
        }
    }
}
