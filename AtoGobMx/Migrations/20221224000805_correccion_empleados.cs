using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_empleados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Antigüedad",
                table: "Empleados",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CódigoEmpleado",
                table: "Empleados",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Estatus",
                table: "Empleados",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "SueldoQuincenal",
                table: "Empleados",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieneExpedienteMédico",
                table: "Empleados",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 23, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Antigüedad",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "CódigoEmpleado",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "SueldoQuincenal",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "TieneExpedienteMédico",
                table: "Empleados");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
