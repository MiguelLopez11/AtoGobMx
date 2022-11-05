using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class estatusVehiculoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstatusEquipoId",
                table: "EstatusVehiculo",
                newName: "EstatusVehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstatusVehiculoId",
                table: "EstatusVehiculo",
                newName: "EstatusEquipoId");
        }
    }
}
