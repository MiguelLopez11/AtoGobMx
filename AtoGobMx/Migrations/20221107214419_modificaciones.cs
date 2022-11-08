using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class modificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nomenclatura",
                table: "ExpedienteAlumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "VeiculoId",
                table: "ExpedienteAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_VeiculoId",
                table: "ExpedienteAlumbrado",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteAlumbrado_PAT_Vehiculo_VeiculoId",
                table: "ExpedienteAlumbrado",
                column: "VeiculoId",
                principalTable: "PAT_Vehiculo",
                principalColumn: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteAlumbrado_PAT_Vehiculo_VeiculoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteAlumbrado_VeiculoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "Nomenclatura",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "VeiculoId",
                table: "ExpedienteAlumbrado");
        }
    }
}
