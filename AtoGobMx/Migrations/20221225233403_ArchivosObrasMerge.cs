using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ArchivosObrasMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArchivosObras_ObraId",
                table: "ArchivosObras",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosCementerios_DireccionId",
                table: "ArchivosCementerios",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAseo_AseoId",
                table: "ArchivosAseo",
                column: "AseoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAseo_Aseo_AseoId",
                table: "ArchivosAseo",
                column: "AseoId",
                principalTable: "Aseo",
                principalColumn: "AseoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosCementerios_DireccionCementerio_DireccionId",
                table: "ArchivosCementerios",
                column: "DireccionId",
                principalTable: "DireccionCementerio",
                principalColumn: "DireccionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosObras_ObrasPublicas_ObraId",
                table: "ArchivosObras",
                column: "ObraId",
                principalTable: "ObrasPublicas",
                principalColumn: "ObraId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAseo_Aseo_AseoId",
                table: "ArchivosAseo");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosCementerios_DireccionCementerio_DireccionId",
                table: "ArchivosCementerios");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosObras_ObrasPublicas_ObraId",
                table: "ArchivosObras");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosObras_ObraId",
                table: "ArchivosObras");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosCementerios_DireccionId",
                table: "ArchivosCementerios");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosAseo_AseoId",
                table: "ArchivosAseo");
        }
    }
}
