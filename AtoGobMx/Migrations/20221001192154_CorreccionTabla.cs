using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class CorreccionTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_EstatusAlumbrado_estatusId",
                table: "Alumbrado");

            migrationBuilder.DropTable(
                name: "EstatusAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_estatusId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "estatusId",
                table: "Alumbrado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estatusId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstatusAlumbrado",
                columns: table => new
                {
                    EstatusAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreEstatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusAlumbrado", x => x.EstatusAlumbradoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_estatusId",
                table: "Alumbrado",
                column: "estatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_EstatusAlumbrado_estatusId",
                table: "Alumbrado",
                column: "estatusId",
                principalTable: "EstatusAlumbrado",
                principalColumn: "EstatusAlumbradoId");
        }
    }
}
