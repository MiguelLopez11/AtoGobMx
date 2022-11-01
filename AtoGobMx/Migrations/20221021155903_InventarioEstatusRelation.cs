using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class InventarioEstatusRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventarioEstatusId",
                table: "Inventario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_InventarioEstatusId",
                table: "Inventario",
                column: "InventarioEstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventario_InventarioEstatus_InventarioEstatusId",
                table: "Inventario",
                column: "InventarioEstatusId",
                principalTable: "InventarioEstatus",
                principalColumn: "InventarioEstatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventario_InventarioEstatus_InventarioEstatusId",
                table: "Inventario");

            migrationBuilder.DropIndex(
                name: "IX_Inventario_InventarioEstatusId",
                table: "Inventario");

            migrationBuilder.DropColumn(
                name: "InventarioEstatusId",
                table: "Inventario");
        }
    }
}
