using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class RelationsUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CURP",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "RFC",
                table: "Empleados");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmarContraseña",
                table: "Usuarios",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExpedienteEmpleado",
                columns: table => new
                {
                    ExpedienteEmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    CURP = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RFC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteEmpleado", x => x.ExpedienteEmpleadoId);
                    table.ForeignKey(
                        name: "FK_ExpedienteEmpleado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_UsuarioId",
                table: "Empleados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteEmpleado_EmpleadoId",
                table: "ExpedienteEmpleado",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Usuarios_UsuarioId",
                table: "Empleados",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Usuarios_UsuarioId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "ExpedienteEmpleado");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_UsuarioId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Empleados");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "ConfirmarContraseña",
                keyValue: null,
                column: "ConfirmarContraseña",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ConfirmarContraseña",
                table: "Usuarios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CURP",
                table: "Empleados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "Empleados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
