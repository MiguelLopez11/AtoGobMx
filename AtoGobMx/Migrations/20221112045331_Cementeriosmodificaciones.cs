using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Cementeriosmodificaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
                table: "Cementerios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cementerios_DireccionId",
                table: "Cementerios",
                column: "DireccionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cementerios_DireccionCementerio_DireccionId",
                table: "Cementerios",
                column: "DireccionId",
                principalTable: "DireccionCementerio",
                principalColumn: "DireccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cementerios_DireccionCementerio_DireccionId",
                table: "Cementerios");

            migrationBuilder.DropIndex(
                name: "IX_Cementerios_DireccionId",
                table: "Cementerios");

            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "Cementerios");
        }
    }
}
