using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class pruebas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Empleados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Area_AreaId",
                table: "Empleados",
                column: "AreaId",
                principalTable: "Area",
                principalColumn: "AreaId");
        }
    }
}
