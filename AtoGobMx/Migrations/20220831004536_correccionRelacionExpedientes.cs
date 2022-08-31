using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccionRelacionExpedientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "expedienteDigitalId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados",
                column: "expedienteDigitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_ExpedienteDigital_expedienteDigitalId",
                table: "Empleados",
                column: "expedienteDigitalId",
                principalTable: "ExpedienteDigital",
                principalColumn: "ExpedienteDigitalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_ExpedienteDigital_expedienteDigitalId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "expedienteDigitalId",
                table: "Empleados");

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
    }
}
