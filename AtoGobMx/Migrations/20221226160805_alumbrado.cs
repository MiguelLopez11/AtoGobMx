using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class alumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_InventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropTable(
                name: "InventarioAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_Alumbrado_InventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "InventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ArchivosAlumbrado",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 26, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ArchivosAlumbrado",
                keyColumn: "Nombre",
                keyValue: null,
                column: "Nombre",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ArchivosAlumbrado",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "InventarioAlumbradoId",
                table: "Alumbrado",
                type: "int",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 25, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_InventarioAlumbradoId",
                table: "Alumbrado",
                column: "InventarioAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_InventarioAlumbradoId",
                table: "Alumbrado",
                column: "InventarioAlumbradoId",
                principalTable: "InventarioAlumbrado",
                principalColumn: "InventarioAlumbradoId");
        }
    }
}
