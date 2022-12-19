using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class removearea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControlDeVales_Area_AreaId",
                table: "ControlDeVales");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipoComputo_Area_AreaId",
                table: "EquipoComputo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpedienteAlumbrado_Area_AreaId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Mobiliario_Area_AreaId",
                table: "Mobiliario");

            migrationBuilder.DropForeignKey(
                name: "FK_PuestoTrabajo_Area_AreaId",
                table: "PuestoTrabajo");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropIndex(
                name: "IX_PuestoTrabajo_AreaId",
                table: "PuestoTrabajo");

            migrationBuilder.DropIndex(
                name: "IX_Mobiliario_AreaId",
                table: "Mobiliario");

            migrationBuilder.DropIndex(
                name: "IX_ExpedienteAlumbrado_AreaId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropIndex(
                name: "IX_EquipoComputo_AreaId",
                table: "EquipoComputo");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_AreaId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_ControlDeVales_AreaId",
                table: "ControlDeVales");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "PuestoTrabajo");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Mobiliario");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "ExpedienteAlumbrado");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "EquipoComputo");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "ControlDeVales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "PuestoTrabajo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Mobiliario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "ExpedienteAlumbrado",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "EquipoComputo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "ControlDeVales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                    table.ForeignKey(
                        name: "FK_Area_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "AreaId", "Archivado", "DepartamentoId", "Descripcion", "Nombre" },
                values: new object[] { 1, false, 1, null, "Soporte TI" });

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "EmpleadoId",
                keyValue: 1,
                column: "AreaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PuestoTrabajo",
                keyColumn: "PuestoTrabajoId",
                keyValue: 1,
                column: "AreaId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_AreaId",
                table: "PuestoTrabajo",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobiliario_AreaId",
                table: "Mobiliario",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_AreaId",
                table: "ExpedienteAlumbrado",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_AreaId",
                table: "EquipoComputo",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_AreaId",
                table: "Empleados",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_AreaId",
                table: "ControlDeVales",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_DepartamentoId",
                table: "Area",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ControlDeVales_Area_AreaId",
                table: "ControlDeVales",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipoComputo_Area_AreaId",
                table: "EquipoComputo",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpedienteAlumbrado_Area_AreaId",
                table: "ExpedienteAlumbrado",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobiliario_Area_AreaId",
                table: "Mobiliario",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PuestoTrabajo_Area_AreaId",
                table: "PuestoTrabajo",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");
        }
    }
}
