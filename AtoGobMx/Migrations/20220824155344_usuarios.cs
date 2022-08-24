using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_UsuarioId",
                table: "Empleados",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Usuarios_UsuarioId",
                table: "Empleados",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Usuarios_UsuarioId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_UsuarioId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Empleados");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
