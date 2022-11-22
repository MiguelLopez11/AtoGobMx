using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class control_de_vale_cambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 18, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
