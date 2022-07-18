using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class relationAreaEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreasId",
                table: "Area",
                newName: "AreaId");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
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
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_AreaId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Empleados");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Area",
                newName: "AreasId");
        }
    }
}
