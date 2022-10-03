using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Estatusdos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estatusId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_estatusId",
                table: "Alumbrado",
                column: "estatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_Estatus_estatusId",
                table: "Alumbrado",
                column: "estatusId",
                principalTable: "Estatus",
                principalColumn: "EstatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_Estatus_estatusId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_estatusId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "estatusId",
                table: "Alumbrado");
        }
    }
}
