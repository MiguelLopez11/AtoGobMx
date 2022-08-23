using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class repararRelacionExpediente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Empleados_EmpleadoId",
                table: "Archivos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_EmpleadoId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Archivos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Archivos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_EmpleadoId",
                table: "Archivos",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Empleados_EmpleadoId",
                table: "Archivos",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId");
        }
    }
}
