using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ArchivosAlumbradoCambios3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArvhivoAlumbradoId",
                table: "ArchivosAlumbrado",
                newName: "ArchivoAlumbradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ArchivoAlumbradoId",
                table: "ArchivosAlumbrado",
                newName: "ArvhivoAlumbradoId");
        }
    }
}
