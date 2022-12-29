using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class puestoTrabajo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PuestoTrabajo",
                keyColumn: "PuestoTrabajoId",
                keyValue: 1,
                column: "Nombre",
                value: "Administrador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PuestoTrabajo",
                keyColumn: "PuestoTrabajoId",
                keyValue: 1,
                column: "Nombre",
                value: "Auxiliar de soporte");
        }
    }
}
