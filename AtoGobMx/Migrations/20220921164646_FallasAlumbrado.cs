using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class FallasAlumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreFalla",
                table: "FallasAlumbradoPublico",
                newName: "TipoFalla");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "FallasAlumbradoPublico",
                newName: "FechaBaja");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "FallasAlumbradoPublico",
                newName: "Domicilio");

            migrationBuilder.AddColumn<string>(
                name: "DescripcionSolucion",
                table: "FallasAlumbradoPublico",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "FallasAlumbradoPublico",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionSolucion",
                table: "FallasAlumbradoPublico");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "FallasAlumbradoPublico");

            migrationBuilder.RenameColumn(
                name: "TipoFalla",
                table: "FallasAlumbradoPublico",
                newName: "NombreFalla");

            migrationBuilder.RenameColumn(
                name: "FechaBaja",
                table: "FallasAlumbradoPublico",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "Domicilio",
                table: "FallasAlumbradoPublico",
                newName: "Descripcion");
        }
    }
}
