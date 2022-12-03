using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class estatus_receta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstatusRecetaId",
                table: "Receta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EstatusReceta",
                columns: table => new
                {
                    EstatusRecetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SERMED_EstatusRecetaEstatusRecetaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusReceta", x => x.EstatusRecetaId);
                    table.ForeignKey(
                        name: "FK_EstatusReceta_EstatusReceta_SERMED_EstatusRecetaEstatusRecet~",
                        column: x => x.SERMED_EstatusRecetaEstatusRecetaId,
                        principalTable: "EstatusReceta",
                        principalColumn: "EstatusRecetaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 2, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Receta_EstatusRecetaId",
                table: "Receta",
                column: "EstatusRecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstatusReceta_SERMED_EstatusRecetaEstatusRecetaId",
                table: "EstatusReceta",
                column: "SERMED_EstatusRecetaEstatusRecetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_EstatusReceta_EstatusRecetaId",
                table: "Receta",
                column: "EstatusRecetaId",
                principalTable: "EstatusReceta",
                principalColumn: "EstatusRecetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receta_EstatusReceta_EstatusRecetaId",
                table: "Receta");

            migrationBuilder.DropTable(
                name: "EstatusReceta");

            migrationBuilder.DropIndex(
                name: "IX_Receta_EstatusRecetaId",
                table: "Receta");

            migrationBuilder.DropColumn(
                name: "EstatusRecetaId",
                table: "Receta");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
