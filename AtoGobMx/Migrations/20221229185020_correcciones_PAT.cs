using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correcciones_PAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccesorioComputo");

            migrationBuilder.DeleteData(
                table: "ExpedienteDigital",
                keyColumn: "ExpedienteDigitalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpedienteMedico",
                keyColumn: "ExpedienteMedicoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccesorioComputo",
                columns: table => new
                {
                    AccesorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Costo = table.Column<double>(type: "double", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccesorioComputo", x => x.AccesorioId);
                    table.ForeignKey(
                        name: "FK_AccesorioComputo_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "EmpleadoId", "Antigüedad", "Archivado", "CódigoEmpleado", "DepartamentoId", "Estatus", "FechaAlta", "FechaBaja", "NombreCompleto", "PuestoTrabajoId", "SueldoQuincenal", "TieneExpediente", "TieneExpedienteMédico" },
                values: new object[] { 1, null, false, null, 1, null, new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Local), null, "Administrador", 1, null, true, false });

            migrationBuilder.InsertData(
                table: "ExpedienteDigital",
                columns: new[] { "ExpedienteDigitalId", "Archivado", "Calle", "CodigoPostal", "CorreoElectronico", "EmpleadoId", "Estado", "FechaNacimiento", "Localidad", "Municipio", "NumeroExterior", "NumeroInterior" },
                values: new object[] { 1, false, null, null, null, 1, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "ExpedienteMedico",
                columns: new[] { "ExpedienteMedicoId", "Alergias", "AntecedentesFamiliares", "AntecedentesPersonales", "Archivado", "Discapacidad", "EmpleadoId", "Estatura", "Peso", "Sexo", "TipoSangre" },
                values: new object[] { 1, null, null, null, false, null, 1, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_AccesorioComputo_EquipoComputoId",
                table: "AccesorioComputo",
                column: "EquipoComputoId");
        }
    }
}
