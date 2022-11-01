using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_estatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventarioEstatusId",
                table: "InventarioEstatus",
                newName: "EstatusEquipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstatusEquipoId",
                table: "InventarioEstatus",
                newName: "InventarioEstatusId");
        }
    }
}
