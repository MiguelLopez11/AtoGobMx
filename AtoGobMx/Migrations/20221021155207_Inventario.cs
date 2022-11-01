using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Inventario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "EstablecimientoPublico",
            //    table: "Aseo",
            //    type: "longtext",
            //    nullable: false)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "RutaId",
                table: "Aseo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriaInventario",
                columns: table => new
                {
                    CategoriaInventarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCategoria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
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
                    Folio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    isOnDependency = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Dependencia = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    CategoriaInventarioId = table.Column<int>(type: "int", nullable: true),
                    Descripción = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "CategoriaInventario");

            migrationBuilder.DropColumn(
                name: "EstablecimientoPublico",
                table: "Aseo");

            migrationBuilder.DropColumn(
                name: "RutaId",
                table: "Aseo");
        }
    }
}
