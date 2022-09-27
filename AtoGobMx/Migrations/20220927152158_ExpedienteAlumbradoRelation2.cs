using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ExpedienteAlumbradoRelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "expedienteAlumbradoId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_expedienteAlumbradoId",
                table: "Alumbrado",
                column: "expedienteAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_expedienteAlumbradoId",
                table: "Alumbrado",
                column: "expedienteAlumbradoId",
                principalTable: "ExpedienteAlumbrado",
                principalColumn: "ExpedienteAlumbradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_expedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_expedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "expedienteAlumbradoId",
                table: "Alumbrado");
        }
    }
}
