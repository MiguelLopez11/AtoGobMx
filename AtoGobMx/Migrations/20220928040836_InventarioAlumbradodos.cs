using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class InventarioAlumbradodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "inventarioAlumbradoId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_inventarioAlumbradoId",
                table: "Alumbrado",
                column: "inventarioAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_inventarioAlumbradoId",
                table: "Alumbrado",
                column: "inventarioAlumbradoId",
                principalTable: "InventarioAlumbrado",
                principalColumn: "InventarioAlumbradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_inventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_inventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "inventarioAlumbradoId",
                table: "Alumbrado");
        }
    }
}
