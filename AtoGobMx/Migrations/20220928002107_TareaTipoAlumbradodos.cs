using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class TareaTipoAlumbradodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tareaTipoId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_tareaTipoId",
                table: "Alumbrado",
                column: "tareaTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_tareaTipoId",
                table: "Alumbrado",
                column: "tareaTipoId",
                principalTable: "TareaTipoAlumbrado",
                principalColumn: "TareaTipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_tareaTipoId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_tareaTipoId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "tareaTipoId",
                table: "Alumbrado");
        }
    }
}
