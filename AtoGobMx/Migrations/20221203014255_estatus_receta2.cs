using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class estatus_receta2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EstatusReceta",
                columns: new[] { "EstatusRecetaId", "Archivado", "Descripcion", "Nombre", "SERMED_EstatusRecetaEstatusRecetaId" },
                values: new object[] { 1, false, null, "Pendiente", null });

            migrationBuilder.InsertData(
                table: "EstatusReceta",
                columns: new[] { "EstatusRecetaId", "Archivado", "Descripcion", "Nombre", "SERMED_EstatusRecetaEstatusRecetaId" },
                values: new object[] { 2, false, null, "Surtido", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatusReceta",
                keyColumn: "EstatusRecetaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EstatusReceta",
                keyColumn: "EstatusRecetaId",
                keyValue: 2);
        }
    }
}
