using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_Armeria_Categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Armeria_CategoriaArmeria_PAT_CategoriaArmeriaCategoriaArmeri~",
                table: "Armeria");

            migrationBuilder.DropTable(
                name: "CategoriaArmeria");

            migrationBuilder.DropIndex(
                name: "IX_Armeria_PAT_CategoriaArmeriaCategoriaArmeriaId",
                table: "Armeria");

            migrationBuilder.DropColumn(
                name: "PAT_CategoriaArmeriaCategoriaArmeriaId",
                table: "Armeria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PAT_CategoriaArmeriaCategoriaArmeriaId",
                table: "Armeria",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoriaArmeria",
                columns: table => new
                {
                    CategoriaArmeriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaArmeria", x => x.CategoriaArmeriaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Armeria_PAT_CategoriaArmeriaCategoriaArmeriaId",
                table: "Armeria",
                column: "PAT_CategoriaArmeriaCategoriaArmeriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Armeria_CategoriaArmeria_PAT_CategoriaArmeriaCategoriaArmeri~",
                table: "Armeria",
                column: "PAT_CategoriaArmeriaCategoriaArmeriaId",
                principalTable: "CategoriaArmeria",
                principalColumn: "CategoriaArmeriaId");
        }
    }
}
