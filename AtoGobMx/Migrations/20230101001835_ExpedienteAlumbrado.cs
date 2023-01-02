using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ExpedienteAlumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TieneExpediente",
                table: "ControlDeVales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ExpedienteVale",
                columns: table => new
                {
                    ExpedienteValeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ControlValeId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteVale", x => x.ExpedienteValeId);
                    table.ForeignKey(
                        name: "FK_ExpedienteVale_ControlDeVales_ControlValeId",
                        column: x => x.ControlValeId,
                        principalTable: "ControlDeVales",
                        principalColumn: "ControlValeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteVale_ControlValeId",
                table: "ExpedienteVale",
                column: "ControlValeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpedienteVale");

            migrationBuilder.DropColumn(
                name: "TieneExpediente",
                table: "ControlDeVales");
        }
    }
}
