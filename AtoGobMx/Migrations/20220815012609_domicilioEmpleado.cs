using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class domicilioEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "Empleados",
                newName: "Municipio");

            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Empleados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NumeroExterior",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroInterior",
                table: "Empleados",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "NumeroExterior",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "NumeroInterior",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "Municipio",
                table: "Empleados",
                newName: "Direccion");
        }
    }
}
