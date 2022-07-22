using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class relationemployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "AreaTrabajoId",
            //    table: "Empleados",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_AreaTrabajoId",
                table: "Empleados",
                column: "AreaTrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreaTrabajoId",
                table: "Empleados",
                column: "AreaTrabajoId",
                principalTable: "Area",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaTrabajoId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_AreaTrabajoId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "AreaTrabajoId",
                table: "Empleados");
        }
    }
}
