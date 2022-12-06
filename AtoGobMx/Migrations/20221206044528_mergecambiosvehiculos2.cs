using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class mergecambiosvehiculos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteAlumbrado_Vehiculo_PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteAlumbrado_PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado",
                column: "PAT_VehiculoVehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteAlumbrado_Vehiculo_PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado",
                column: "PAT_VehiculoVehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId");
        }
    }
}
