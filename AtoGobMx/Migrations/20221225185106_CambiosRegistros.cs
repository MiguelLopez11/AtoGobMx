using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class CambiosRegistros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAlumbrado_Alumbrado_AlumbradoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAlumbrado_Aseo_AseoId",
                table: "ArchivosAlumbrado");

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

            migrationBuilder.DropIndex(
                name: "IX_ArchivosAlumbrado_AlumbradoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosAlumbrado_AseoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.DropColumn(
                name: "AseoId",
                table: "ArchivosAlumbrado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AseoId",
                table: "ArchivosAlumbrado",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAlumbrado_AlumbradoId",
                table: "ArchivosAlumbrado",
                column: "AlumbradoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAlumbrado_AseoId",
                table: "ArchivosAlumbrado",
                column: "AseoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAlumbrado_Alumbrado_AlumbradoId",
                table: "ArchivosAlumbrado",
                column: "AlumbradoId",
                principalTable: "Alumbrado",
                principalColumn: "AlumbradoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAlumbrado_Aseo_AseoId",
                table: "ArchivosAlumbrado",
                column: "AseoId",
                principalTable: "Aseo",
                principalColumn: "AseoId");

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
    }
}
