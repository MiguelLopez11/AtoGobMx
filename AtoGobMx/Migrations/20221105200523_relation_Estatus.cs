using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class relation_Estatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstatusVehiculoId",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_EstatusVehiculoId",
                table: "Vehiculo",
                column: "EstatusVehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                table: "Vehiculo",
                column: "EstatusVehiculoId",
                principalTable: "EstatusVehiculo",
                principalColumn: "EstatusVehiculoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_EstatusVehiculoId",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "EstatusVehiculoId",
                table: "Vehiculo");
        }
    }
}
