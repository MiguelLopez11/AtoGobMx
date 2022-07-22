using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class fechasempleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Empleados_Area_AreaId",
            //    table: "Empleados");

            //migrationBuilder.DropColumn(
            //    name: "FechaAlta",
            //    table: "Area");

            //migrationBuilder.DropColumn(
            //    name: "FechaBaja",
            //    table: "Area");

            //migrationBuilder.RenameColumn(
            //    name: "AreaId",
            //    table: "Empleados",
            //    newName: "AreaTrabajoId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Empleados_AreaId",
            //    table: "Empleados",
            //newName: "IX_Empleados_AreaTrabajoId");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Nombre",
            //    table: "Empleados",
            //    type: "varchar(100)",
            //    maxLength: 100,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "longtext")
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ApellidoPaterno",
            //    table: "Empleados",
            //    type: "varchar(100)",
            //    maxLength: 100,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "longtext")
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.AlterColumn<string>(
            //    name: "ApellidoMaterno",
            //    table: "Empleados",
            //    type: "varchar(100)",
            //    maxLength: 100,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "longtext")
            //    .Annotation("MySql:CharSet", "utf8mb4")
            //    .OldAnnotation("MySql:CharSet", "utf8mb4");
            //__________________________
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "Empleados",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Area",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreaTrabajoId",
                table: "Empleados",
                column: "AreaTrabajoId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaTrabajoId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Area");

            migrationBuilder.RenameColumn(
                name: "AreaTrabajoId",
                table: "Empleados",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_AreaTrabajoId",
                table: "Empleados",
                newName: "IX_Empleados_AreaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Empleados",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoPaterno",
                table: "Empleados",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ApellidoMaterno",
                table: "Empleados",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Area",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaBaja",
                table: "Area",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
