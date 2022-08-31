using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class expedienteempleados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "ExpedienteDigital",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteDigital_Empleados_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteDigital_Empleados_EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "ExpedienteDigital");
        }
    }
}
