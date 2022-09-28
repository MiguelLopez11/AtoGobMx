using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class PuestotrabajoEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PuestoTrabajoId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_PuestoTrabajoId",
                table: "Empleados",
                column: "PuestoTrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_PuestoTrabajo_PuestoTrabajoId",
                table: "Empleados",
                column: "PuestoTrabajoId",
                principalTable: "PuestoTrabajo",
                principalColumn: "PuestoTrabajoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_PuestoTrabajo_PuestoTrabajoId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_PuestoTrabajoId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "PuestoTrabajoId",
                table: "Empleados");
        }
    }
}
