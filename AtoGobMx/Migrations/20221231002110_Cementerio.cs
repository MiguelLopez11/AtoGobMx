using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Cementerio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosCementerios_DireccionCementerio_DireccionId",
                table: "ArchivosCementerios");

            migrationBuilder.DropForeignKey(
                name: "FK_Gabetas_DireccionCementerio_DireccionId",
                table: "Gabetas");

            migrationBuilder.DropTable(
                name: "DireccionCementerio");

            migrationBuilder.RenameColumn(
                name: "DireccionId",
                table: "Gabetas",
                newName: "CementerioId");

            migrationBuilder.RenameIndex(
                name: "IX_Gabetas_DireccionId",
                table: "Gabetas",
                newName: "IX_Gabetas_CementerioId");

            migrationBuilder.RenameColumn(
                name: "DireccionId",
                table: "ArchivosCementerios",
                newName: "CementerioId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchivosCementerios_DireccionId",
                table: "ArchivosCementerios",
                newName: "IX_ArchivosCementerios_CementerioId");

            migrationBuilder.CreateTable(
                name: "Cementerio",
                columns: table => new
                {
                    CementerioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCementerio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comunidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroExterior = table.Column<int>(type: "int", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cementerio", x => x.CementerioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosCementerios_Cementerio_CementerioId",
                table: "ArchivosCementerios",
                column: "CementerioId",
                principalTable: "Cementerio",
                principalColumn: "CementerioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gabetas_Cementerio_CementerioId",
                table: "Gabetas",
                column: "CementerioId",
                principalTable: "Cementerio",
                principalColumn: "CementerioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosCementerios_Cementerio_CementerioId",
                table: "ArchivosCementerios");

            migrationBuilder.DropForeignKey(
                name: "FK_Gabetas_Cementerio_CementerioId",
                table: "Gabetas");

            migrationBuilder.DropTable(
                name: "Cementerio");

            migrationBuilder.RenameColumn(
                name: "CementerioId",
                table: "Gabetas",
                newName: "DireccionId");

            migrationBuilder.RenameIndex(
                name: "IX_Gabetas_CementerioId",
                table: "Gabetas",
                newName: "IX_Gabetas_DireccionId");

            migrationBuilder.RenameColumn(
                name: "CementerioId",
                table: "ArchivosCementerios",
                newName: "DireccionId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchivosCementerios_CementerioId",
                table: "ArchivosCementerios",
                newName: "IX_ArchivosCementerios_DireccionId");

            migrationBuilder.CreateTable(
                name: "DireccionCementerio",
                columns: table => new
                {
                    DireccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Calle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    Localidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Municipio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreCementerio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroExterior = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionCementerio", x => x.DireccionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosCementerios_DireccionCementerio_DireccionId",
                table: "ArchivosCementerios",
                column: "DireccionId",
                principalTable: "DireccionCementerio",
                principalColumn: "DireccionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gabetas_DireccionCementerio_DireccionId",
                table: "Gabetas",
                column: "DireccionId",
                principalTable: "DireccionCementerio",
                principalColumn: "DireccionId");
        }
    }
}
