using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ControldeValesmodificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_DepartamentoId",
                table: "ControlDeVales",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_EmpleadoId",
                table: "ControlDeVales",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_EstatusValeId",
                table: "ControlDeVales",
                column: "EstatusValeId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_ProductoId",
                table: "ControlDeVales",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_ProveedorId",
                table: "ControlDeVales",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_TipoId",
                table: "ControlDeVales",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_Departamentos_DepartamentoId",
                table: "ControlDeVales",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_Empleados_EmpleadoId",
                table: "ControlDeVales",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_PROV_EstatusVale_EstatusValeId",
                table: "ControlDeVales",
                column: "EstatusValeId",
                principalTable: "PROV_EstatusVale",
                principalColumn: "EstatusValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_PROV_Producto_ProductoId",
                table: "ControlDeVales",
                column: "ProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_PROV_Proveedor_ProveedorId",
                table: "ControlDeVales",
                column: "ProveedorId",
                principalTable: "PROV_Proveedor",
                principalColumn: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_TipoVales_TipoId",
                table: "ControlDeVales",
                column: "TipoId",
                principalTable: "TipoVales",
                principalColumn: "TipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_Departamentos_DepartamentoId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_Empleados_EmpleadoId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_PROV_EstatusVale_EstatusValeId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_PROV_Producto_ProductoId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_PROV_Proveedor_ProveedorId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_TipoVales_TipoId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_DepartamentoId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_EmpleadoId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_EstatusValeId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_ProductoId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_ProveedorId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_TipoId",
                table: "ControlDeVales");
        }
    }
}
