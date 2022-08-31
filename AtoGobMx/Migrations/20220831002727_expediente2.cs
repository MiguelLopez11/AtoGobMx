using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class expediente2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_ExpedienteDigital_EmpleadoId",
            //    table: "ExpedienteDigital");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExpedienteDigital_EmpleadoId",
            //    table: "ExpedienteDigital",
            //    column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId",
                unique: true);
        }
    }
}
