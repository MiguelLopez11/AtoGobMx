using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ArchivosAlumbradoMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAlumbrado_AlumbradoId",
                table: "ArchivosAlumbrado",
                column: "AlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAlumbrado_Alumbrado_AlumbradoId",
                table: "ArchivosAlumbrado",
                column: "AlumbradoId",
                principalTable: "Alumbrado",
                principalColumn: "AlumbradoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAlumbrado_Alumbrado_AlumbradoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosAlumbrado_AlumbradoId",
                table: "ArchivosAlumbrado");
        }
    }
}
