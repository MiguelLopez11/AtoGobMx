using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_Cita_SERMED : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCitante",
                table: "Cita");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.InsertData(
                table: "ExpedienteDigital",
                columns: new[] { "ExpedienteDigitalId", "Archivado", "Calle", "CodigoPostal", "CorreoElectronico", "EmpleadoId", "Estado", "FechaNacimiento", "Localidad", "Municipio", "NumeroExterior", "NumeroInterior" },
                values: new object[] { 1, false, null, null, null, 1, null, null, null, null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpedienteDigital",
                keyColumn: "ExpedienteDigitalId",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "NombreCitante",
                table: "Cita",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
