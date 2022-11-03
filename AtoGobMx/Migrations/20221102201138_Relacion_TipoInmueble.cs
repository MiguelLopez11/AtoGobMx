using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Relacion_TipoInmueble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Mobiliario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoMobiliarioId",
                table: "Mobiliario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mobiliario_DepartamentoId",
                table: "Mobiliario",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobiliario_TipoMobiliarioId",
                table: "Mobiliario",
                column: "TipoMobiliarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobiliario_Departamentos_DepartamentoId",
                table: "Mobiliario",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mobiliario_TipoMobiliario_TipoMobiliarioId",
                table: "Mobiliario",
                column: "TipoMobiliarioId",
                principalTable: "TipoMobiliario",
                principalColumn: "TipoMobiliarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobiliario_Departamentos_DepartamentoId",
                table: "Mobiliario");

            migrationBuilder.DropForeignKey(
                name: "FK_Mobiliario_TipoMobiliario_TipoMobiliarioId",
                table: "Mobiliario");

            migrationBuilder.DropIndex(
                name: "IX_Mobiliario_DepartamentoId",
                table: "Mobiliario");

            migrationBuilder.DropIndex(
                name: "IX_Mobiliario_TipoMobiliarioId",
                table: "Mobiliario");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Mobiliario");

            migrationBuilder.DropColumn(
                name: "TipoMobiliarioId",
                table: "Mobiliario");
        }
    }
}
