using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_vales3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "PROV_DetalleVale");

            migrationBuilder.AddColumn<float>(
                name: "Precio",
                table: "PROV_DetalleProducto",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "PROV_DetalleProducto");

            migrationBuilder.AddColumn<float>(
                name: "Precio",
                table: "PROV_DetalleVale",
                type: "float",
                nullable: true);
        }
    }
}
