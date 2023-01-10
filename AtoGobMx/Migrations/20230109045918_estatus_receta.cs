using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class estatus_receta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstatusReceta_EstatusReceta_SERMED_EstatusRecetaEstatusRecet~",
                table: "EstatusReceta");

            migrationBuilder.DropIndex(
                name: "IX_EstatusReceta_SERMED_EstatusRecetaEstatusRecetaId",
                table: "EstatusReceta");

            migrationBuilder.DropColumn(
                name: "SERMED_EstatusRecetaEstatusRecetaId",
                table: "EstatusReceta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SERMED_EstatusRecetaEstatusRecetaId",
                table: "EstatusReceta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstatusReceta_SERMED_EstatusRecetaEstatusRecetaId",
                table: "EstatusReceta",
                column: "SERMED_EstatusRecetaEstatusRecetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstatusReceta_EstatusReceta_SERMED_EstatusRecetaEstatusRecet~",
                table: "EstatusReceta",
                column: "SERMED_EstatusRecetaEstatusRecetaId",
                principalTable: "EstatusReceta",
                principalColumn: "EstatusRecetaId");
        }
    }
}
