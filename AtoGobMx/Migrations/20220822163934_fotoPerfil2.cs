using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class fotoPerfil2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteDigital_Archivos_FotoPerfil",
                table: "ExpedienteDigital");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteDigital_FotoPerfil",
                table: "ExpedienteDigital");

            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "ExpedienteDigital");

            migrationBuilder.AddColumn<int>(
                name: "ExpedienteDigitalId",
                table: "Archivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_ExpedienteDigitalId",
                table: "Archivos",
                column: "ExpedienteDigitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_ExpedienteDigital_ExpedienteDigitalId",
                table: "Archivos",
                column: "ExpedienteDigitalId",
                principalTable: "ExpedienteDigital",
                principalColumn: "ExpedienteDigitalId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_ExpedienteDigital_ExpedienteDigitalId",
                table: "Archivos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_ExpedienteDigitalId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "ExpedienteDigitalId",
                table: "Archivos");

            migrationBuilder.AddColumn<int>(
                name: "FotoPerfil",
                table: "ExpedienteDigital",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_FotoPerfil",
                table: "ExpedienteDigital",
                column: "FotoPerfil");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteDigital_Archivos_FotoPerfil",
                table: "ExpedienteDigital",
                column: "FotoPerfil",
                principalTable: "Archivos",
                principalColumn: "ArchivoId");
        }
    }
}
