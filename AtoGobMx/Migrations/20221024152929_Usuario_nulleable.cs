using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Usuario_nulleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RoleId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RutaId",
                table: "Aseo");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RoleId",
                table: "Usuarios",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_RoleId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RutaId",
                table: "Aseo",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_RoleId",
                table: "Usuarios",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
