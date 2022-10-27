using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Mobiliario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "EquipoComputo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PAT_Mobiliario",
                columns: table => new
                {
                    MobiliarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreMobiliario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripción = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Mobiliario", x => x.MobiliarioId);
                    table.ForeignKey(
                        name: "FK_PAT_Mobiliario_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_AreaId",
                table: "EquipoComputo",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Mobiliario_AreaId",
                table: "PAT_Mobiliario",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipoComputo_Area_AreaId",
                table: "EquipoComputo",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipoComputo_Area_AreaId",
                table: "EquipoComputo");

            migrationBuilder.DropTable(
                name: "PAT_Mobiliario");

            migrationBuilder.DropIndex(
                name: "IX_EquipoComputo_AreaId",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "EquipoComputo");
        }
    }
}
