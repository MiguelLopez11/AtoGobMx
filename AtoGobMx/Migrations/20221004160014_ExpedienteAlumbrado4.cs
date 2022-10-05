using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ExpedienteAlumbrado4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "ExpedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.AddColumn<int>(
                name: "AlumbradoId",
                table: "ExpedienteAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_AlumbradoId",
                table: "ExpedienteAlumbrado",
                column: "AlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteAlumbrado_Alumbrado_AlumbradoId",
                table: "ExpedienteAlumbrado",
                column: "AlumbradoId",
                principalTable: "Alumbrado",
                principalColumn: "AlumbradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteAlumbrado_Alumbrado_AlumbradoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteAlumbrado_AlumbradoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "AlumbradoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.AddColumn<int>(
                name: "ExpedienteAlumbradoId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado",
                column: "ExpedienteAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado",
                column: "ExpedienteAlumbradoId",
                principalTable: "ExpedienteAlumbrado",
                principalColumn: "ExpedienteAlumbradoId");
        }
    }
}
