using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class AseoVehiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AseoVehiculo",
                columns: table => new
                {
                    AseoVehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RutaId = table.Column<int>(type: "int", nullable: true),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AseoVehiculo", x => x.AseoVehiculoId);
                    table.ForeignKey(
                        name: "FK_AseoVehiculo_Ruta_RutaId",
                        column: x => x.RutaId,
                        principalTable: "Ruta",
                        principalColumn: "RutaId");
                    table.ForeignKey(
                        name: "FK_AseoVehiculo_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AseoVehiculo_RutaId",
                table: "AseoVehiculo",
                column: "RutaId");

            migrationBuilder.CreateIndex(
                name: "IX_AseoVehiculo_VehiculoId",
                table: "AseoVehiculo",
                column: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AseoVehiculo");
        }
    }
}
