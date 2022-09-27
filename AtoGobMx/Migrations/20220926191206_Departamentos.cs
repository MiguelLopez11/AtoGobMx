using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Departamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "PuestoTrabajo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_DepartamentoId",
                table: "PuestoTrabajo",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PuestoTrabajo_Departamentos_DepartamentoId",
                table: "PuestoTrabajo",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PuestoTrabajo_Departamentos_DepartamentoId",
                table: "PuestoTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_PuestoTrabajo_DepartamentoId",
                table: "PuestoTrabajo");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "PuestoTrabajo");
        }
    }
}
