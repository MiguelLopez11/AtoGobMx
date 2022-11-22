using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Obras_publicas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "PROV_Producto");

            migrationBuilder.DropColumn(
                name: "subtotal",
                table: "PROV_DetalleVale");

            migrationBuilder.DropColumn(
                name: "TipoCombustible",
                table: "ControlDeVales");

            migrationBuilder.RenameColumn(
                name: "NombreVale",
                table: "TipoVales",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "EstatusVale",
                table: "PROV_EstatusVale",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "NombreEstatus",
                table: "OP_EstatusObras",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "NombreObra",
                table: "ObrasPublicas",
                newName: "Nombre");

            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "PROV_Proveedor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "PROV_Producto",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<float>(
                name: "Precio",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "IVA",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<int>(
                name: "DetalleValeId",
                table: "ControlDeVales",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "ControlDeVales",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_AreaId",
                table: "ControlDeVales",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_DetalleValeId",
                table: "ControlDeVales",
                column: "DetalleValeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_Area_AreaId",
                table: "ControlDeVales",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_PROV_DetalleVale_DetalleValeId",
                table: "ControlDeVales",
                column: "DetalleValeId",
                principalTable: "PROV_DetalleVale",
                principalColumn: "DetalleValeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_Area_AreaId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_PROV_DetalleVale_DetalleValeId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_AreaId",
                table: "ControlDeVales");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_DetalleValeId",
                table: "ControlDeVales");

            migrationBuilder.DropColumn(
                name: "RFC",
                table: "PROV_Proveedor");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "PROV_Producto");

            migrationBuilder.DropColumn(
                name: "IVA",
                table: "PROV_DetalleVale");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "PROV_DetalleVale");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "ControlDeVales");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "TipoVales",
                newName: "NombreVale");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "PROV_EstatusVale",
                newName: "EstatusVale");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "OP_EstatusObras",
                newName: "NombreEstatus");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "ObrasPublicas",
                newName: "NombreObra");

            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "PROV_Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                table: "PROV_DetalleVale",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "subtotal",
                table: "PROV_DetalleVale",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<float>(
                name: "DetalleValeId",
                table: "ControlDeVales",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoCombustible",
                table: "ControlDeVales",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 14, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
