using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correcciones342 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipoComputo_InventarioEstatus_EstatusId",
                table: "EquipoComputo");

            migrationBuilder.RenameColumn(
                name: "EstatusId",
                table: "EquipoComputo",
                newName: "EstatusEquipoId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipoComputo_EstatusId",
                table: "EquipoComputo",
                newName: "IX_EquipoComputo_EstatusEquipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipoComputo_InventarioEstatus_EstatusEquipoId",
                table: "EquipoComputo",
                column: "EstatusEquipoId",
                principalTable: "InventarioEstatus",
                principalColumn: "EstatusEquipoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipoComputo_InventarioEstatus_EstatusEquipoId",
                table: "EquipoComputo");

            migrationBuilder.RenameColumn(
                name: "EstatusEquipoId",
                table: "EquipoComputo",
                newName: "EstatusId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipoComputo_EstatusEquipoId",
                table: "EquipoComputo",
                newName: "IX_EquipoComputo_EstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipoComputo_InventarioEstatus_EstatusId",
                table: "EquipoComputo",
                column: "EstatusId",
                principalTable: "InventarioEstatus",
                principalColumn: "EstatusEquipoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
