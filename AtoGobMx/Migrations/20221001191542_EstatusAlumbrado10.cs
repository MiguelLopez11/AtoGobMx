using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class EstatusAlumbrado10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstatusId",
                table: "EstatusAlumbrado",
                newName: "EstatusAlumbradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstatusAlumbradoId",
                table: "EstatusAlumbrado",
                newName: "EstatusId");
        }
    }
}
