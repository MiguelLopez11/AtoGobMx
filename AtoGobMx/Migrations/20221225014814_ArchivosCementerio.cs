using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ArchivosCementerio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchivosCementerios",
                columns: table => new
                {
                    ArchivosCementerioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoArchivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DireccionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosCementerios", x => x.ArchivosCementerioId);
                    table.ForeignKey(
                        name: "FK_ArchivosCementerios_DireccionCementerio_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "DireccionCementerio",
                        principalColumn: "DireccionId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosCementerios_DireccionId",
                table: "ArchivosCementerios",
                column: "DireccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivosCementerios");
        }
    }
}
