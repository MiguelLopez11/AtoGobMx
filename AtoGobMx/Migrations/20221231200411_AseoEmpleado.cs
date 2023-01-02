using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class AseoEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AseoEmpleado",
                columns: table => new
                {
                    AseoEmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    RutaId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AseoEmpleado", x => x.AseoEmpleadoId);
                    table.ForeignKey(
                        name: "FK_AseoEmpleado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_AseoEmpleado_Ruta_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Ruta",
                        principalColumn: "RutaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AseoEmpleado_EmpleadoId",
                table: "AseoEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AseoEmpleado_RutaId",
                table: "AseoEmpleado",
                column: "RutaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AseoEmpleado");
        }
    }
}
