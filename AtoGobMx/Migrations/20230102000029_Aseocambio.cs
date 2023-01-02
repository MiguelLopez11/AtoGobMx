using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Aseocambio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Objetivo",
                table: "Aseo",
                newName: "Problema");

            migrationBuilder.RenameColumn(
                name: "NombreServicio",
                table: "Aseo",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "EstablecimientoPublico",
                table: "Aseo",
                newName: "Descripcion");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Aseo",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Aseo");

            migrationBuilder.RenameColumn(
                name: "Problema",
                table: "Aseo",
                newName: "Objetivo");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Aseo",
                newName: "NombreServicio");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Aseo",
                newName: "EstablecimientoPublico");
        }
    }
}
