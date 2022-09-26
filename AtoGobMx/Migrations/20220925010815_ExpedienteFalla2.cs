using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ExpedienteFalla2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "expedienteFallaId",
                table: "FallasAlumbradoPublico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FallasAlumbradoPublico_expedienteFallaId",
                table: "FallasAlumbradoPublico",
                column: "expedienteFallaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FallasAlumbradoPublico_ExpedienteFalla_expedienteFallaId",
                table: "FallasAlumbradoPublico",
                column: "expedienteFallaId",
                principalTable: "ExpedienteFalla",
                principalColumn: "ExpedienteFallaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FallasAlumbradoPublico_ExpedienteFalla_expedienteFallaId",
                table: "FallasAlumbradoPublico");

            migrationBuilder.DropIndex(
                name: "IX_FallasAlumbradoPublico_expedienteFallaId",
                table: "FallasAlumbradoPublico");

            migrationBuilder.DropColumn(
                name: "expedienteFallaId",
                table: "FallasAlumbradoPublico");
        }
    }
}
