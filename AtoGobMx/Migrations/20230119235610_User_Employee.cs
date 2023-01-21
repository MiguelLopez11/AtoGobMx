using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class User_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmpleadoId",
                table: "AspNetUsers",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empleados_EmpleadoId",
                table: "AspNetUsers",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empleados_EmpleadoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmpleadoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "AspNetUsers");
        }
    }
}
