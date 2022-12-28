using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class equiposComputacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PAT_AccesorioComputo_EquipoComputo_EquipoComputoId",
                table: "PAT_AccesorioComputo");

            migrationBuilder.DropTable(
                name: "Monitor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAT_AccesorioComputo",
                table: "PAT_AccesorioComputo");

            migrationBuilder.RenameTable(
                name: "PAT_AccesorioComputo",
                newName: "AccesorioComputo");

            migrationBuilder.RenameIndex(
                name: "IX_PAT_AccesorioComputo_EquipoComputoId",
                table: "AccesorioComputo",
                newName: "IX_AccesorioComputo_EquipoComputoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccesorioComputo",
                table: "AccesorioComputo",
                column: "AccesorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccesorioComputo_EquipoComputo_EquipoComputoId",
                table: "AccesorioComputo",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccesorioComputo_EquipoComputo_EquipoComputoId",
                table: "AccesorioComputo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccesorioComputo",
                table: "AccesorioComputo");

            migrationBuilder.RenameTable(
                name: "AccesorioComputo",
                newName: "PAT_AccesorioComputo");

            migrationBuilder.RenameIndex(
                name: "IX_AccesorioComputo_EquipoComputoId",
                table: "PAT_AccesorioComputo",
                newName: "IX_PAT_AccesorioComputo_EquipoComputoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAT_AccesorioComputo",
                table: "PAT_AccesorioComputo",
                column: "AccesorioId");

            migrationBuilder.CreateTable(
                name: "Monitor",
                columns: table => new
                {
                    MonitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Costo = table.Column<double>(type: "double", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroSerie = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pulgadas = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor", x => x.MonitorId);
                    table.ForeignKey(
                        name: "FK_Monitor_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_EquipoComputoId",
                table: "Monitor",
                column: "EquipoComputoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PAT_AccesorioComputo_EquipoComputo_EquipoComputoId",
                table: "PAT_AccesorioComputo",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
