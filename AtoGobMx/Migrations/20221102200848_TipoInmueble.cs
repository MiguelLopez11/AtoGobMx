using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class TipoInmueble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PAT_Mobiliario_Area_AreaId",
                table: "PAT_Mobiliario");

            migrationBuilder.DropForeignKey(
                name: "FK_PAT_Monitor_EquipoComputo_EquipoComputoId",
                table: "PAT_Monitor");

            migrationBuilder.DropForeignKey(
                name: "FK_PAT_Mouse_EquipoComputo_EquipoComputoId",
                table: "PAT_Mouse");

            migrationBuilder.DropForeignKey(
                name: "FK_PAT_Teclado_EquipoComputo_EquipoComputoId",
                table: "PAT_Teclado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAT_Vehiculo",
                table: "PAT_Vehiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAT_Teclado",
                table: "PAT_Teclado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAT_Mouse",
                table: "PAT_Mouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAT_Monitor",
                table: "PAT_Monitor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PAT_Mobiliario",
                table: "PAT_Mobiliario");

            migrationBuilder.RenameTable(
                name: "PAT_Vehiculo",
                newName: "Vehiculo");

            migrationBuilder.RenameTable(
                name: "PAT_Teclado",
                newName: "Teclado");

            migrationBuilder.RenameTable(
                name: "PAT_Mouse",
                newName: "Mouse");

            migrationBuilder.RenameTable(
                name: "PAT_Monitor",
                newName: "Monitor");

            migrationBuilder.RenameTable(
                name: "PAT_Mobiliario",
                newName: "Mobiliario");

            migrationBuilder.RenameIndex(
                name: "IX_PAT_Teclado_EquipoComputoId",
                table: "Teclado",
                newName: "IX_Teclado_EquipoComputoId");

            migrationBuilder.RenameIndex(
                name: "IX_PAT_Mouse_EquipoComputoId",
                table: "Mouse",
                newName: "IX_Mouse_EquipoComputoId");

            migrationBuilder.RenameIndex(
                name: "IX_PAT_Monitor_EquipoComputoId",
                table: "Monitor",
                newName: "IX_Monitor_EquipoComputoId");

            migrationBuilder.RenameIndex(
                name: "IX_PAT_Mobiliario_AreaId",
                table: "Mobiliario",
                newName: "IX_Mobiliario_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehiculo",
                table: "Vehiculo",
                column: "VehiculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teclado",
                table: "Teclado",
                column: "TecladoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mouse",
                table: "Mouse",
                column: "MouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Monitor",
                table: "Monitor",
                column: "MonitorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mobiliario",
                table: "Mobiliario",
                column: "MobiliarioId");

            migrationBuilder.CreateTable(
                name: "TipoMobiliario",
                columns: table => new
                {
                    TipoMobiliarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripción = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMobiliario", x => x.TipoMobiliarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobiliario_Area_AreaId",
                table: "Mobiliario",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Monitor_EquipoComputo_EquipoComputoId",
                table: "Monitor",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mouse_EquipoComputo_EquipoComputoId",
                table: "Mouse",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teclado_EquipoComputo_EquipoComputoId",
                table: "Teclado",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobiliario_Area_AreaId",
                table: "Mobiliario");

            migrationBuilder.DropForeignKey(
                name: "FK_Monitor_EquipoComputo_EquipoComputoId",
                table: "Monitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Mouse_EquipoComputo_EquipoComputoId",
                table: "Mouse");

            migrationBuilder.DropForeignKey(
                name: "FK_Teclado_EquipoComputo_EquipoComputoId",
                table: "Teclado");

            migrationBuilder.DropTable(
                name: "TipoMobiliario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehiculo",
                table: "Vehiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teclado",
                table: "Teclado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mouse",
                table: "Mouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Monitor",
                table: "Monitor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mobiliario",
                table: "Mobiliario");

            migrationBuilder.RenameTable(
                name: "Vehiculo",
                newName: "PAT_Vehiculo");

            migrationBuilder.RenameTable(
                name: "Teclado",
                newName: "PAT_Teclado");

            migrationBuilder.RenameTable(
                name: "Mouse",
                newName: "PAT_Mouse");

            migrationBuilder.RenameTable(
                name: "Monitor",
                newName: "PAT_Monitor");

            migrationBuilder.RenameTable(
                name: "Mobiliario",
                newName: "PAT_Mobiliario");

            migrationBuilder.RenameIndex(
                name: "IX_Teclado_EquipoComputoId",
                table: "PAT_Teclado",
                newName: "IX_PAT_Teclado_EquipoComputoId");

            migrationBuilder.RenameIndex(
                name: "IX_Mouse_EquipoComputoId",
                table: "PAT_Mouse",
                newName: "IX_PAT_Mouse_EquipoComputoId");

            migrationBuilder.RenameIndex(
                name: "IX_Monitor_EquipoComputoId",
                table: "PAT_Monitor",
                newName: "IX_PAT_Monitor_EquipoComputoId");

            migrationBuilder.RenameIndex(
                name: "IX_Mobiliario_AreaId",
                table: "PAT_Mobiliario",
                newName: "IX_PAT_Mobiliario_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAT_Vehiculo",
                table: "PAT_Vehiculo",
                column: "VehiculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAT_Teclado",
                table: "PAT_Teclado",
                column: "TecladoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAT_Mouse",
                table: "PAT_Mouse",
                column: "MouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAT_Monitor",
                table: "PAT_Monitor",
                column: "MonitorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PAT_Mobiliario",
                table: "PAT_Mobiliario",
                column: "MobiliarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PAT_Mobiliario_Area_AreaId",
                table: "PAT_Mobiliario",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PAT_Monitor_EquipoComputo_EquipoComputoId",
                table: "PAT_Monitor",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PAT_Mouse_EquipoComputo_EquipoComputoId",
                table: "PAT_Mouse",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PAT_Teclado_EquipoComputo_EquipoComputoId",
                table: "PAT_Teclado",
                column: "EquipoComputoId",
                principalTable: "EquipoComputo",
                principalColumn: "EquipoComputoId");
        }
    }
}
