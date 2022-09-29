using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class EstatusAlumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_ExpedienteDigital_expedienteDigitalId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "expedienteDigitalId",
                table: "Empleados",
                newName: "PuestoTrabajoId");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_expedienteDigitalId",
                table: "Empleados",
                newName: "IX_Empleados_PuestoTrabajoId");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "PuestoTrabajo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "ExpedienteDigital",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "ExpedienteDigital",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Departamentos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_DepartamentoId",
                table: "PuestoTrabajo",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_DepartamentoId",
                table: "Empleados",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Departamentos_DepartamentoId",
                table: "Empleados",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_PuestoTrabajo_PuestoTrabajoId",
                table: "Empleados",
                column: "PuestoTrabajoId",
                principalTable: "PuestoTrabajo",
                principalColumn: "PuestoTrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteDigital_Empleados_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PuestoTrabajo_Departamentos_DepartamentoId",
                table: "PuestoTrabajo",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "DepartamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Departamentos_DepartamentoId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_PuestoTrabajo_PuestoTrabajoId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteDigital_Empleados_EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.DropForeignKey(
                name: "FK_PuestoTrabajo_Departamentos_DepartamentoId",
                table: "PuestoTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_PuestoTrabajo_DepartamentoId",
                table: "PuestoTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_DepartamentoId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "PuestoTrabajo");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "ExpedienteDigital");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "PuestoTrabajoId",
                table: "Empleados",
                newName: "expedienteDigitalId");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_PuestoTrabajoId",
                table: "Empleados",
                newName: "IX_Empleados_expedienteDigitalId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Empleados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Departamentos",
                keyColumn: "Descripcion",
                keyValue: null,
                column: "Descripcion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Departamentos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_ExpedienteDigital_expedienteDigitalId",
                table: "Empleados",
                column: "expedienteDigitalId",
                principalTable: "ExpedienteDigital",
                principalColumn: "ExpedienteDigitalId");
        }
    }
}
