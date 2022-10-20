using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class empleado_alumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmpleadosAlumbrado",
                columns: table => new
                {
                    AlumbradoEmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    ExpedienteAlumbradoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosAlumbrado", x => x.AlumbradoEmpleadoId);
                    table.ForeignKey(
                        name: "FK_EmpleadosAlumbrado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_EmpleadosAlumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                        column: x => x.ExpedienteAlumbradoId,
                        principalTable: "ExpedienteAlumbrado",
                        principalColumn: "ExpedienteAlumbradoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadosAlumbrado_EmpleadoId",
                table: "EmpleadosAlumbrado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadosAlumbrado_ExpedienteAlumbradoId",
                table: "EmpleadosAlumbrado",
                column: "ExpedienteAlumbradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadosAlumbrado");
        }
    }
}
