using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class EstatusVehiculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaArmeria",
                columns: table => new
                {
                    CategoriaArmeriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaArmeria", x => x.CategoriaArmeriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstatusVehiculo",
                columns: table => new
                {
                    EstatusEquipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusVehiculo", x => x.EstatusEquipoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Armeria",
                columns: table => new
                {
                    ArmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nomenclatura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaArmeriaId = table.Column<int>(type: "int", nullable: false),
                    Calibre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    PAT_CategoriaArmeriaCategoriaArmeriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armeria", x => x.ArmaId);
                    table.ForeignKey(
                        name: "FK_Armeria_CategoriaArmeria_PAT_CategoriaArmeriaCategoriaArmeri~",
                        column: x => x.PAT_CategoriaArmeriaCategoriaArmeriaId,
                        principalTable: "CategoriaArmeria",
                        principalColumn: "CategoriaArmeriaId");
                    table.ForeignKey(
                        name: "FK_Armeria_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Armeria_EmpleadoId",
                table: "Armeria",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Armeria_PAT_CategoriaArmeriaCategoriaArmeriaId",
                table: "Armeria",
                column: "PAT_CategoriaArmeriaCategoriaArmeriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Armeria");

            migrationBuilder.DropTable(
                name: "EstatusVehiculo");

            migrationBuilder.DropTable(
                name: "CategoriaArmeria");
        }
    }
}
