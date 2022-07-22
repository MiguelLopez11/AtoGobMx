using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class relationemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaTrabajoId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_AreaTrabajoId",
                table: "Empleados");

            //migrationBuilder.DropColumn(
            //    name: "AreaId",
            //    table: "Empleados");

            migrationBuilder.DropColumn(
                name: "AreaTrabajoId",
                table: "Empleados");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AreaTrabajoId",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_AreaId",
                table: "Empleados",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");
        }
    }
}
