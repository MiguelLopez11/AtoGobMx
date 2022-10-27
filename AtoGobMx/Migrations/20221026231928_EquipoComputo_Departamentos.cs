using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class EquipoComputo_Departamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "EquipoComputo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_DepartamentoId",
                table: "EquipoComputo",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipoComputo_Departamentos_DepartamentoId",
                table: "EquipoComputo",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipoComputo_Departamentos_DepartamentoId",
                table: "EquipoComputo");

            migrationBuilder.DropIndex(
                name: "IX_EquipoComputo_DepartamentoId",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "EquipoComputo");
        }
    }
}
