using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ArchivosAseo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AseoId",
                table: "ArchivosAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArchivosAseo",
                columns: table => new
                {
                    ArchivosAseoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoArchivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AseoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivosAseo", x => x.ArchivosAseoId);
                    table.ForeignKey(
                        name: "FK_ArchivosAseo_Aseo_AseoId",
                        column: x => x.AseoId,
                        principalTable: "Aseo",
                        principalColumn: "AseoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAlumbrado_AseoId",
                table: "ArchivosAlumbrado",
                column: "AseoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivosAseo_AseoId",
                table: "ArchivosAseo",
                column: "AseoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchivosAlumbrado_Aseo_AseoId",
                table: "ArchivosAlumbrado",
                column: "AseoId",
                principalTable: "Aseo",
                principalColumn: "AseoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchivosAlumbrado_Aseo_AseoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.DropTable(
                name: "ArchivosAseo");

            migrationBuilder.DropIndex(
                name: "IX_ArchivosAlumbrado_AseoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.DropColumn(
                name: "AseoId",
                table: "ArchivosAlumbrado");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 24, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
