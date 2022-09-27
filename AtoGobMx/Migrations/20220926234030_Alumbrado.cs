using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Alumbrado : Migration
    {
        public int AlumbradoId { get; internal set; }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FallasAlumbradoPublico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FallasAlumbradoPublico",
                columns: table => new
                {
                    FallaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExpedienteFallaId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DescripcionDomicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescripcionSolucion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TipoFalla = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FallasAlumbradoPublico", x => x.FallaId);
                    table.ForeignKey(
                        name: "FK_FallasAlumbradoPublico_ExpedienteFalla_ExpedienteFallaId",
                        column: x => x.ExpedienteFallaId,
                        principalTable: "ExpedienteFalla",
                        principalColumn: "ExpedienteFallaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FallasAlumbradoPublico_ExpedienteFallaId",
                table: "FallasAlumbradoPublico",
                column: "ExpedienteFallaId");
        }
    }
}
