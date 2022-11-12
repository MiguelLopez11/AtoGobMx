using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class OP_EstatusObras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OP_EstatusObras",
                columns: table => new
                {
                    EstatusObraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreEstatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OP_EstatusObras", x => x.EstatusObraId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ObrasPublicas_EstatusObraId",
                table: "ObrasPublicas",
                column: "EstatusObraId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObrasPublicas_OP_EstatusObras_EstatusObraId",
                table: "ObrasPublicas",
                column: "EstatusObraId",
                principalTable: "OP_EstatusObras",
                principalColumn: "EstatusObraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObrasPublicas_OP_EstatusObras_EstatusObraId",
                table: "ObrasPublicas");

            migrationBuilder.DropTable(
                name: "OP_EstatusObras");

            migrationBuilder.DropIndex(
                name: "IX_ObrasPublicas_EstatusObraId",
                table: "ObrasPublicas");
        }
    }
}
