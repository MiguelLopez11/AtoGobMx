using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Patrimonio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departamentos",
                keyColumn: "DepartamentoId",
                keyValue: 1,
                column: "Nombre",
                value: "Comisaría de Seguridad Pública");

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "DepartamentoId", "Archivado", "Descripcion", "Nombre" },
                values: new object[] { 2, false, null, "Seguridad Pública" });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "DepartamentoId", "Archivado", "Descripcion", "Nombre" },
                values: new object[] { 3, false, null, "Sistemas y tecnologías de la información" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "DepartamentoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "DepartamentoId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Departamentos",
                keyColumn: "DepartamentoId",
                keyValue: 1,
                column: "Nombre",
                value: "Direccion de Sistemas");
        }
    }
}
