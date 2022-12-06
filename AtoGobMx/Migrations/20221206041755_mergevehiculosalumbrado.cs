using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class mergevehiculosalumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehiculosAlumbrado_EstatusVehiculo_EstatusVehiculoId",
                table: "VehiculosAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_VehiculosAlumbrado_EstatusVehiculoId",
                table: "VehiculosAlumbrado");

            migrationBuilder.DropColumn(
                name: "EstatusVehiculoId",
                table: "VehiculosAlumbrado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstatusVehiculoId",
                table: "VehiculosAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosAlumbrado_EstatusVehiculoId",
                table: "VehiculosAlumbrado",
                column: "EstatusVehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehiculosAlumbrado_EstatusVehiculo_EstatusVehiculoId",
                table: "VehiculosAlumbrado",
                column: "EstatusVehiculoId",
                principalTable: "EstatusVehiculo",
                principalColumn: "EstatusVehiculoId");
        }
    }
}
