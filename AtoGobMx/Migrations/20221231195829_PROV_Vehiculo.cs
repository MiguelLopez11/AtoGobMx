using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class PROV_Vehiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROV_Vehiculo",
                columns: table => new
                {
                    ProvVehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    ControlValeId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_Vehiculo", x => x.ProvVehiculoId);
                    table.ForeignKey(
                        name: "FK_PROV_Vehiculo_ControlDeVales_ControlValeId",
                        column: x => x.ControlValeId,
                        principalTable: "ControlDeVales",
                        principalColumn: "ControlValeId");
                    table.ForeignKey(
                        name: "FK_PROV_Vehiculo_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_Vehiculo_ControlValeId",
                table: "PROV_Vehiculo",
                column: "ControlValeId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_Vehiculo_VehiculoId",
                table: "PROV_Vehiculo",
                column: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROV_Vehiculo");
        }
    }
}
