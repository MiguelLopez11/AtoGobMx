using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class AlumbradoPublico21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoAlumbradoTareaTipoId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_TareaTipoAlumbradoTareaTipoId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "TareaTipoAlumbradoTareaTipoId",
                table: "Alumbrado");

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_TareaTipoId",
                table: "Alumbrado",
                column: "TareaTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoId",
                table: "Alumbrado",
                column: "TareaTipoId",
                principalTable: "TareaTipoAlumbrado",
                principalColumn: "TareaTipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_TareaTipoId",
                table: "Alumbrado");

            migrationBuilder.AddColumn<int>(
                name: "TareaTipoAlumbradoTareaTipoId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_TareaTipoAlumbradoTareaTipoId",
                table: "Alumbrado",
                column: "TareaTipoAlumbradoTareaTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoAlumbradoTareaTipoId",
                table: "Alumbrado",
                column: "TareaTipoAlumbradoTareaTipoId",
                principalTable: "TareaTipoAlumbrado",
                principalColumn: "TareaTipoId");
        }
    }
}
