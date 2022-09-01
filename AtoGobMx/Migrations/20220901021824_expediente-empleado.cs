using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class expedienteempleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados",
                column: "expedienteDigitalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados",
                column: "expedienteDigitalId",
                unique: true);
        }
    }
}
