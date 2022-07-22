using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class areaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Empleados_Area_AreasId",
            //    table: "Empleados");

            //migrationBuilder.RenameColumn(
            //    name: "AreasId",
            //    table: "Empleados",
            //    newName: "AreaId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Empleados_AreasId",
            //    table: "Empleados",
            //    newName: "IX_Empleados_AreaId");

            migrationBuilder.RenameColumn(
                name: "AreasId",
                table: "Area",
                newName: "AreaId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Empleados_Area_AreaId",
            //    table: "Empleados",
            //    column: "AreaId",
            //    principalTable: "Area",
            //    principalColumn: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Empleados",
                newName: "AreasId");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_AreaId",
                table: "Empleados",
                newName: "IX_Empleados_AreasId");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Area",
                newName: "AreasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreasId",
                table: "Empleados",
                column: "AreasId",
                principalTable: "Area",
                principalColumn: "AreasId");
        }
    }
}
