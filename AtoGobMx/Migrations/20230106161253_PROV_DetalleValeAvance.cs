using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class PROV_DetalleValeAvance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Medida",
                table: "PROV_DetalleVale",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "PROV_DetalleVale",
                type: "double",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Medida",
                table: "PROV_DetalleVale");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "PROV_DetalleVale");
        }
    }
}
