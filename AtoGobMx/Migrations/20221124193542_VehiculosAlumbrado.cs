using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class VehiculosAlumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehiculosAlumbrado",
                columns: table => new
                {
                    VehiculoAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EstatusVehiculoId = table.Column<int>(type: "int", nullable: true),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    ExpedienteAlumbradoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculosAlumbrado", x => x.VehiculoAlumbradoId);
                    table.ForeignKey(
                        name: "FK_VehiculosAlumbrado_EstatusVehiculo_EstatusVehiculoId",
                        column: x => x.EstatusVehiculoId,
                        principalTable: "EstatusVehiculo",
                        principalColumn: "EstatusVehiculoId");
                    table.ForeignKey(
                        name: "FK_VehiculosAlumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                        column: x => x.ExpedienteAlumbradoId,
                        principalTable: "ExpedienteAlumbrado",
                        principalColumn: "ExpedienteAlumbradoId");
                    table.ForeignKey(
                        name: "FK_VehiculosAlumbrado_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 24, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosAlumbrado_EstatusVehiculoId",
                table: "VehiculosAlumbrado",
                column: "EstatusVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosAlumbrado_ExpedienteAlumbradoId",
                table: "VehiculosAlumbrado",
                column: "ExpedienteAlumbradoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosAlumbrado_VehiculoId",
                table: "VehiculosAlumbrado",
                column: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiculosAlumbrado");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "FechaAlta",
                value: new DateTime(2022, 11, 22, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
