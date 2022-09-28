using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correcciones_Empleado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "ExpedienteDigital");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "ExpedienteDigital");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "Empleados",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "Empleados");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "ExpedienteDigital",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "ExpedienteDigital",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
