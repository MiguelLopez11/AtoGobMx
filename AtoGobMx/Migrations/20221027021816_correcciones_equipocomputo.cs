using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correcciones_equipocomputo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoInventario",
                table: "PAT_Teclado");

            migrationBuilder.DropColumn(
                name: "CodigoInventario",
                table: "PAT_Mouse");

            migrationBuilder.DropColumn(
                name: "CodigoInventario",
                table: "PAT_Monitor");

            migrationBuilder.AddColumn<string>(
                name: "CodigoInventario",
                table: "EquipoComputo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoInventario",
                table: "EquipoComputo");

            migrationBuilder.AddColumn<string>(
                name: "CodigoInventario",
                table: "PAT_Teclado",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CodigoInventario",
                table: "PAT_Mouse",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CodigoInventario",
                table: "PAT_Monitor",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
