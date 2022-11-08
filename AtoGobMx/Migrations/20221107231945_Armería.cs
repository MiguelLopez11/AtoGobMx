using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Armería : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaArmeriaId",
                table: "Armeria");

            migrationBuilder.AlterColumn<int>(
                name: "Calibre",
                table: "Armeria",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Armeria",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoArma",
                table: "Armeria",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Armeria");

            migrationBuilder.DropColumn(
                name: "TipoArma",
                table: "Armeria");

            migrationBuilder.AlterColumn<string>(
                name: "Calibre",
                table: "Armeria",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaArmeriaId",
                table: "Armeria",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
