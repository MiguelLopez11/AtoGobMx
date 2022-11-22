using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Recetas_ServiciosMedicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SERMED_ProductosReceta_Medicamentos_SERMED_ProductoProductoId",
                table: "SERMED_ProductosReceta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SERMED_ProductosReceta",
                table: "SERMED_ProductosReceta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicamentos",
                table: "Medicamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Citas",
                table: "Citas");

            migrationBuilder.RenameTable(
                name: "SERMED_ProductosReceta",
                newName: "ProductoReceta");

            migrationBuilder.RenameTable(
                name: "Medicamentos",
                newName: "Medicamento");

            migrationBuilder.RenameTable(
                name: "Citas",
                newName: "Cita");

            migrationBuilder.RenameIndex(
                name: "IX_SERMED_ProductosReceta_SERMED_ProductoProductoId",
                table: "ProductoReceta",
                newName: "IX_ProductoReceta_SERMED_ProductoProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoReceta",
                table: "ProductoReceta",
                column: "ProductoRecetaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicamento",
                table: "Medicamento",
                column: "ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cita",
                table: "Cita",
                column: "CitaId");

            migrationBuilder.CreateTable(
                name: "ExpedienteMedico",
                columns: table => new
                {
                    ExpedienteMedicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Estatura = table.Column<float>(type: "float", nullable: false),
                    Peso = table.Column<float>(type: "float", nullable: false),
                    TipoSangre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sexo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alergias = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discapacidad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AntecedentesPersonales = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AntecedentesFamiliares = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteMedico", x => x.ExpedienteMedicoId);
                    table.ForeignKey(
                        name: "FK_ExpedienteMedico_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    RecetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    diagnostico = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.RecetaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteMedico_EmpleadoId",
                table: "ExpedienteMedico",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoReceta_Medicamento_SERMED_ProductoProductoId",
                table: "ProductoReceta",
                column: "SERMED_ProductoProductoId",
                principalTable: "Medicamento",
                principalColumn: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoReceta_Medicamento_SERMED_ProductoProductoId",
                table: "ProductoReceta");

            migrationBuilder.DropTable(
                name: "ExpedienteMedico");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoReceta",
                table: "ProductoReceta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicamento",
                table: "Medicamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cita",
                table: "Cita");

            migrationBuilder.RenameTable(
                name: "ProductoReceta",
                newName: "SERMED_ProductosReceta");

            migrationBuilder.RenameTable(
                name: "Medicamento",
                newName: "Medicamentos");

            migrationBuilder.RenameTable(
                name: "Cita",
                newName: "Citas");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoReceta_SERMED_ProductoProductoId",
                table: "SERMED_ProductosReceta",
                newName: "IX_SERMED_ProductosReceta_SERMED_ProductoProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SERMED_ProductosReceta",
                table: "SERMED_ProductosReceta",
                column: "ProductoRecetaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicamentos",
                table: "Medicamentos",
                column: "ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Citas",
                table: "Citas",
                column: "CitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_SERMED_ProductosReceta_Medicamentos_SERMED_ProductoProductoId",
                table: "SERMED_ProductosReceta",
                column: "SERMED_ProductoProductoId",
                principalTable: "Medicamentos",
                principalColumn: "ProductoId");
        }
    }
}
