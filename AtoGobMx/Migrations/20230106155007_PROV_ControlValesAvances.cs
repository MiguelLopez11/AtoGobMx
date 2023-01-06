using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class PROV_ControlValesAvances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TieneExpediente",
                table: "ControlDeVales");

            migrationBuilder.AddColumn<string>(
                name: "Dependencia",
                table: "ControlDeVales",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Subprograma",
                table: "ControlDeVales",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dependencia",
                table: "ControlDeVales");

            migrationBuilder.DropColumn(
                name: "Subprograma",
                table: "ControlDeVales");

            migrationBuilder.AddColumn<bool>(
                name: "TieneExpediente",
                table: "ControlDeVales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
