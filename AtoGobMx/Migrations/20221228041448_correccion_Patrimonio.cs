using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion_Patrimonio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "InventarioAlumbrado");

            migrationBuilder.DropTable(
                name: "Mouse");

            migrationBuilder.DropTable(
                name: "Teclado");

            migrationBuilder.DropColumn(
                name: "Almacenamiento",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "MemoriaRAM",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "Procesador",
                table: "EquipoComputo");

            migrationBuilder.AddColumn<double>(
                name: "Costo",
                table: "Monitor",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Monitor",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "Monitor",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Caracteristicas",
                table: "EquipoComputo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Costo",
                table: "EquipoComputo",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroSerie",
                table: "EquipoComputo",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaAdquisicion",
                table: "EquipoComputo",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PAT_AccesorioComputo",
                columns: table => new
                {
                    AccesorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Costo = table.Column<double>(type: "double", nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_AccesorioComputo", x => x.AccesorioId);
                    table.ForeignKey(
                        name: "FK_PAT_AccesorioComputo_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_PAT_AccesorioComputo_EquipoComputoId",
                table: "PAT_AccesorioComputo",
                column: "EquipoComputoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAT_AccesorioComputo");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Monitor");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Monitor");

            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "Monitor");

            migrationBuilder.DropColumn(
                name: "Caracteristicas",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "NumeroSerie",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "fechaAdquisicion",
                table: "EquipoComputo");

            migrationBuilder.AddColumn<string>(
                name: "Almacenamiento",
                table: "EquipoComputo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "EquipoComputo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MemoriaRAM",
                table: "EquipoComputo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Procesador",
                table: "EquipoComputo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventarioAlumbrado",
                columns: table => new
                {
                    InventarioAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioAlumbrado", x => x.InventarioAlumbradoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mouse",
                columns: table => new
                {
                    MouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConexion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mouse", x => x.MouseId);
                    table.ForeignKey(
                        name: "FK_Mouse_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teclado",
                columns: table => new
                {
                    TecladoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConexion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teclado", x => x.TecladoId);
                    table.ForeignKey(
                        name: "FK_Teclado_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Mouse_EquipoComputoId",
                table: "Mouse",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_Teclado_EquipoComputoId",
                table: "Teclado",
                column: "EquipoComputoId");
        }
    }
}
