using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class RenameTableEmpleados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "archivado",
                table: "Empleados",
                newName: "Archivado");

            migrationBuilder.RenameColumn(
                name: "apellidoPaterno",
                table: "Empleados",
                newName: "ApellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "apellidoMaterno",
                table: "Empleados",
                newName: "ApellidoMaterno");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Archivado",
                table: "Empleados",
                newName: "archivado");

            migrationBuilder.RenameColumn(
                name: "ApellidoPaterno",
                table: "Empleados",
                newName: "apellidoPaterno");

            migrationBuilder.RenameColumn(
                name: "ApellidoMaterno",
                table: "Empleados",
                newName: "apellidoMaterno");
        }
    }
}
