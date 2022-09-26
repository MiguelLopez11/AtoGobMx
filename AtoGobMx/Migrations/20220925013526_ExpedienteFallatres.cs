using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ExpedienteFallatres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FallasAlumbradoPublico_ExpedienteFalla_expedienteFallaId",
                table: "FallasAlumbradoPublico");

            migrationBuilder.RenameColumn(
                name: "expedienteFallaId",
                table: "FallasAlumbradoPublico",
                newName: "ExpedienteFallaId");

            migrationBuilder.RenameIndex(
                name: "IX_FallasAlumbradoPublico_expedienteFallaId",
                table: "FallasAlumbradoPublico",
                newName: "IX_FallasAlumbradoPublico_ExpedienteFallaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FallasAlumbradoPublico_ExpedienteFalla_ExpedienteFallaId",
                table: "FallasAlumbradoPublico",
                column: "ExpedienteFallaId",
                principalTable: "ExpedienteFalla",
                principalColumn: "ExpedienteFallaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FallasAlumbradoPublico_ExpedienteFalla_ExpedienteFallaId",
                table: "FallasAlumbradoPublico");

            migrationBuilder.RenameColumn(
                name: "ExpedienteFallaId",
                table: "FallasAlumbradoPublico",
                newName: "expedienteFallaId");

            migrationBuilder.RenameIndex(
                name: "IX_FallasAlumbradoPublico_ExpedienteFallaId",
                table: "FallasAlumbradoPublico",
                newName: "IX_FallasAlumbradoPublico_expedienteFallaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FallasAlumbradoPublico_ExpedienteFalla_expedienteFallaId",
                table: "FallasAlumbradoPublico",
                column: "expedienteFallaId",
                principalTable: "ExpedienteFalla",
                principalColumn: "ExpedienteFallaId");
        }
    }
}
