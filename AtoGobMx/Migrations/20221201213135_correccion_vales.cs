using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_vales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_ProductoId",
                table: "PROV_DetalleVale");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "PROV_DetalleVale",
                newName: "PROV_ProductoProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_PROV_DetalleVale_ProductoId",
                table: "PROV_DetalleVale",
                newName: "IX_PROV_DetalleVale_PROV_ProductoProductoId");

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "IVA",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "PROV_DetalleVale",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PROV_ControlValesControlValeId",
                table: "PROV_DetalleVale",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleVale_PROV_ControlValesControlValeId",
                table: "PROV_DetalleVale",
                column: "PROV_ControlValesControlValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_ControlDeVales_PROV_ControlValesControlVale~",
                table: "PROV_DetalleVale",
                column: "PROV_ControlValesControlValeId",
                principalTable: "ControlDeVales",
                principalColumn: "ControlValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                column: "PROV_ProductoProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_ControlDeVales_PROV_ControlValesControlVale~",
                table: "PROV_DetalleVale");

            migrationBuilder.DropForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_PROV_ProductoProductoId",
                table: "PROV_DetalleVale");

            migrationBuilder.DropIndex(
                name: "IX_PROV_DetalleVale_PROV_ControlValesControlValeId",
                table: "PROV_DetalleVale");

            migrationBuilder.DropColumn(
                name: "PROV_ControlValesControlValeId",
                table: "PROV_DetalleVale");

            migrationBuilder.RenameColumn(
                name: "PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_PROV_DetalleVale_PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                newName: "IX_PROV_DetalleVale_ProductoId");

            migrationBuilder.AlterColumn<float>(
                name: "Total",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "IVA",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "PROV_DetalleVale",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_PROV_DetalleVale_PROV_Producto_ProductoId",
                table: "PROV_DetalleVale",
                column: "ProductoId",
                principalTable: "PROV_Producto",
                principalColumn: "ProductoId");
        }
    }
}
