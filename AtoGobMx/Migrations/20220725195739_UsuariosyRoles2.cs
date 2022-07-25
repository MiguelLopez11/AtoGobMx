using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class UsuariosyRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RollId",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.AddColumn<bool>(
                name: "Archivado",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivado",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "RollId");
        }
    }
}
