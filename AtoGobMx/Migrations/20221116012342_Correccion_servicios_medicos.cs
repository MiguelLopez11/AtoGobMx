using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Correccion_servicios_medicos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoReceta_Medicamento_SERMED_ProductoProductoId",
                table: "ProductoReceta");

            migrationBuilder.DropIndex(
                name: "IX_ProductoReceta_SERMED_ProductoProductoId",
                table: "ProductoReceta");

            migrationBuilder.DropColumn(
                name: "SERMED_ProductoProductoId",
                table: "ProductoReceta");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Cita",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receta_EmpleadoId",
                table: "Receta",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReceta_ProductoId",
                table: "ProductoReceta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReceta_RecetaId",
                table: "ProductoReceta",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EmpleadoId",
                table: "Cita",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Empleados_EmpleadoId",
                table: "Cita",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoReceta_Medicamento_ProductoId",
                table: "ProductoReceta",
                column: "ProductoId",
                principalTable: "Medicamento",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoReceta_Receta_RecetaId",
                table: "ProductoReceta",
                column: "RecetaId",
                principalTable: "Receta",
                principalColumn: "RecetaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receta_Empleados_EmpleadoId",
                table: "Receta",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Empleados_EmpleadoId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoReceta_Medicamento_ProductoId",
                table: "ProductoReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoReceta_Receta_RecetaId",
                table: "ProductoReceta");

            migrationBuilder.DropForeignKey(
                name: "FK_Receta_Empleados_EmpleadoId",
                table: "Receta");

            migrationBuilder.DropIndex(
                name: "IX_Receta_EmpleadoId",
                table: "Receta");

            migrationBuilder.DropIndex(
                name: "IX_ProductoReceta_ProductoId",
                table: "ProductoReceta");

            migrationBuilder.DropIndex(
                name: "IX_ProductoReceta_RecetaId",
                table: "ProductoReceta");

            migrationBuilder.DropIndex(
                name: "IX_Cita_EmpleadoId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Cita");

            migrationBuilder.AddColumn<int>(
                name: "SERMED_ProductoProductoId",
                table: "ProductoReceta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReceta_SERMED_ProductoProductoId",
                table: "ProductoReceta",
                column: "SERMED_ProductoProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoReceta_Medicamento_SERMED_ProductoProductoId",
                table: "ProductoReceta",
                column: "SERMED_ProductoProductoId",
                principalTable: "Medicamento",
                principalColumn: "ProductoId");
        }
    }
}
