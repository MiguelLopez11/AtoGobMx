using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correcciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                table: "Vehiculo");

            migrationBuilder.AlterColumn<int>(
                name: "EstatusVehiculoId",
                table: "Vehiculo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                table: "Vehiculo",
                column: "EstatusVehiculoId",
                principalTable: "EstatusVehiculo",
                principalColumn: "EstatusVehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                table: "Vehiculo");

            migrationBuilder.AlterColumn<int>(
                name: "EstatusVehiculoId",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 12, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                table: "Vehiculo",
                column: "EstatusVehiculoId",
                principalTable: "EstatusVehiculo",
                principalColumn: "EstatusVehiculoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
