using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class coordsRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Obsevacion",
                table: "Ruta",
                newName: "Observacion");

            migrationBuilder.AddColumn<string>(
                name: "Horario",
                table: "Ruta",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Ruta",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horario",
                table: "Ruta");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Ruta");

            migrationBuilder.RenameColumn(
                name: "Observacion",
                table: "Ruta",
                newName: "Obsevacion");
        }
    }
}
