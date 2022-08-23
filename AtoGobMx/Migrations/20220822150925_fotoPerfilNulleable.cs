using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class fotoPerfilNulleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteDigital_Archivos_FotoPerfil",
                table: "ExpedienteDigital");

            migrationBuilder.AlterColumn<int>(
                name: "FotoPerfil",
                table: "ExpedienteDigital",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteDigital_Archivos_FotoPerfil",
                table: "ExpedienteDigital",
                column: "FotoPerfil",
                principalTable: "Archivos",
                principalColumn: "ArchivoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteDigital_Archivos_FotoPerfil",
                table: "ExpedienteDigital");

            migrationBuilder.AlterColumn<int>(
                name: "FotoPerfil",
                table: "ExpedienteDigital",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteDigital_Archivos_FotoPerfil",
                table: "ExpedienteDigital",
                column: "FotoPerfil",
                principalTable: "Archivos",
                principalColumn: "ArchivoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
