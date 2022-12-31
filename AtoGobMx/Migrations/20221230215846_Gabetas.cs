using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Gabetas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cementerios");

            migrationBuilder.CreateTable(
                name: "Gabetas",
                columns: table => new
                {
                    GabetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePropietario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroEspasios = table.Column<int>(type: "int", nullable: false),
                    MetrosCorrespondientes = table.Column<float>(type: "float", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    EspaciosDisponibles = table.Column<int>(type: "int", nullable: false),
                    DireccionId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gabetas", x => x.GabetaId);
                    table.ForeignKey(
                        name: "FK_Gabetas_DireccionCementerio_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "DireccionCementerio",
                        principalColumn: "DireccionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Gabetas_DireccionId",
                table: "Gabetas",
                column: "DireccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gabetas");

            migrationBuilder.CreateTable(
                name: "Cementerios",
                columns: table => new
                {
                    CementeriosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DireccionId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EspaciosDisponibles = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    MetrosCorrespondientes = table.Column<float>(type: "float", nullable: false),
                    NombrePropietario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroEspasios = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cementerios", x => x.CementeriosId);
                    table.ForeignKey(
                        name: "FK_Cementerios_DireccionCementerio_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "DireccionCementerio",
                        principalColumn: "DireccionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cementerios_DireccionId",
                table: "Cementerios",
                column: "DireccionId");
        }
    }
}
