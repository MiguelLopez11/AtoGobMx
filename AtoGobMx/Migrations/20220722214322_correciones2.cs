using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correciones2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "FechaAlta",
            //    table: "Empleados");

            //migrationBuilder.DropColumn(
            //    name: "FechaBaja",
            //    table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Empleados",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Empleados",
                newName: "fechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Empleados",
                newName: "direccion");

            migrationBuilder.RenameColumn(
                name: "ApellidoPaterno",
                table: "Empleados",
                newName: "apellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "ApellidoMaterno",
                table: "Empleados",
                newName: "apellidoMaterno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "Empleados",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "fechaNacimiento",
                table: "Empleados",
                newName: "FechaNacimiento");

            migrationBuilder.RenameColumn(
                name: "direccion",
                table: "Empleados",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "apellidoPaterno",
                table: "Empleados",
                newName: "ApellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "apellidoMaterno",
                table: "Empleados",
                newName: "ApellidoMaterno");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "Empleados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
