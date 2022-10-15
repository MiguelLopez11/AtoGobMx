using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class AlumbradoPublico12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "InventarioAlumbrado");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "InventarioAlumbrado");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "InventarioAlumbrado");

            migrationBuilder.DropColumn(
                name: "Tarea",
                table: "InventarioAlumbrado");

            migrationBuilder.DropColumn(
                name: "DescripcionDomicilio",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "Domicilio",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "Tarea",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "DescripcionSolucion",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Alumbrado");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "Alumbrado");

            migrationBuilder.RenameColumn(
                name: "Tarea",
                table: "Alumbrado",
                newName: "DescripcionProblema");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "ExpedienteAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "ExpedienteAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localidad",
                table: "ExpedienteAlumbrado",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LugarPublico",
                table: "ExpedienteAlumbrado",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DescripcionDomicilio",
                table: "Alumbrado",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_AreaId",
                table: "ExpedienteAlumbrado",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_DepartamentoId",
                table: "ExpedienteAlumbrado",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteAlumbrado_Area_AreaId",
                table: "ExpedienteAlumbrado",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteAlumbrado_Departamentos_DepartamentoId",
                table: "ExpedienteAlumbrado",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteAlumbrado_Area_AreaId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteAlumbrado_Departamentos_DepartamentoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteAlumbrado_AreaId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteAlumbrado_DepartamentoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "Localidad",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "LugarPublico",
                table: "ExpedienteAlumbrado");

            migrationBuilder.RenameColumn(
                name: "DescripcionProblema",
                table: "Alumbrado",
                newName: "Tarea");

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "InventarioAlumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "InventarioAlumbrado",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "InventarioAlumbrado",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Tarea",
                table: "InventarioAlumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DescripcionDomicilio",
                table: "ExpedienteAlumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Domicilio",
                table: "ExpedienteAlumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Tarea",
                table: "ExpedienteAlumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Alumbrado",
                keyColumn: "DescripcionDomicilio",
                keyValue: null,
                column: "DescripcionDomicilio",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DescripcionDomicilio",
                table: "Alumbrado",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DescripcionSolucion",
                table: "Alumbrado",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Alumbrado",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "Alumbrado",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
