using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class EquiposComputo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "CategoriaInventario");

            migrationBuilder.CreateTable(
                name: "EquipoComputo",
                columns: table => new
                {
                    EquipoComputoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemoriaRAM = table.Column<int>(type: "int", nullable: false),
                    Almacenamiento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Procesador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoComputo", x => x.EquipoComputoId);
                    table.ForeignKey(
                        name: "FK_EquipoComputo_InventarioEstatus_EstatusId",
                        column: x => x.EstatusId,
                        principalTable: "InventarioEstatus",
                        principalColumn: "InventarioEstatusId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Monitor",
                columns: table => new
                {
                    MonitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoInventario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pulgadas = table.Column<int>(type: "int", nullable: true),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Monitor", x => x.MonitorId);
                    table.ForeignKey(
                        name: "FK_PAT_Monitor_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Mouse",
                columns: table => new
                {
                    MouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoInventario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConexion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Mouse", x => x.MouseId);
                    table.ForeignKey(
                        name: "FK_PAT_Mouse_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Teclado",
                columns: table => new
                {
                    TecladoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoInventario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConexion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Teclado", x => x.TecladoId);
                    table.ForeignKey(
                        name: "FK_PAT_Teclado_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_EstatusId",
                table: "EquipoComputo",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Monitor_EquipoComputoId",
                table: "PAT_Monitor",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Mouse_EquipoComputoId",
                table: "PAT_Mouse",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Teclado_EquipoComputoId",
                table: "PAT_Teclado",
                column: "EquipoComputoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAT_Monitor");

            migrationBuilder.DropTable(
                name: "PAT_Mouse");

            migrationBuilder.DropTable(
                name: "PAT_Teclado");

            migrationBuilder.DropTable(
                name: "EquipoComputo");

            migrationBuilder.CreateTable(
                name: "CategoriaInventario",
                columns: table => new
                {
                    CategoriaInventarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreCategoria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaInventario", x => x.CategoriaInventarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    InventarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoriaInventarioId = table.Column<int>(type: "int", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    InventarioEstatusId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Dependencia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripción = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Folio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isOnDependency = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.InventarioId);
                    table.ForeignKey(
                        name: "FK_Inventario_CategoriaInventario_CategoriaInventarioId",
                        column: x => x.CategoriaInventarioId,
                        principalTable: "CategoriaInventario",
                        principalColumn: "CategoriaInventarioId");
                    table.ForeignKey(
                        name: "FK_Inventario_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId");
                    table.ForeignKey(
                        name: "FK_Inventario_InventarioEstatus_InventarioEstatusId",
                        column: x => x.InventarioEstatusId,
                        principalTable: "InventarioEstatus",
                        principalColumn: "InventarioEstatusId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_CategoriaInventarioId",
                table: "Inventario",
                column: "CategoriaInventarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_DepartamentoId",
                table: "Inventario",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_InventarioEstatusId",
                table: "Inventario",
                column: "InventarioEstatusId");
        }
    }
}
