using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_EstatusAlumbrado_estatusId",
                table: "Alumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_expedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_inventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_tareaTipoId",
                table: "Alumbrado");

            migrationBuilder.RenameColumn(
                name: "tareaTipoId",
                table: "Alumbrado",
                newName: "TareaTipoId");

            migrationBuilder.RenameColumn(
                name: "inventarioAlumbradoId",
                table: "Alumbrado",
                newName: "InventarioAlumbradoId");

            migrationBuilder.RenameColumn(
                name: "expedienteAlumbradoId",
                table: "Alumbrado",
                newName: "ExpedienteAlumbradoId");

            migrationBuilder.RenameColumn(
                name: "estatusId",
                table: "Alumbrado",
                newName: "EstatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_tareaTipoId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_TareaTipoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_inventarioAlumbradoId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_InventarioAlumbradoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_expedienteAlumbradoId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_ExpedienteAlumbradoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_estatusId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_EstatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_EstatusAlumbrado_EstatusId",
                table: "Alumbrado",
                column: "EstatusId",
                principalTable: "EstatusAlumbrado",
                principalColumn: "EstatusAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado",
                column: "ExpedienteAlumbradoId",
                principalTable: "ExpedienteAlumbrado",
                principalColumn: "ExpedienteAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_InventarioAlumbradoId",
                table: "Alumbrado",
                column: "InventarioAlumbradoId",
                principalTable: "InventarioAlumbrado",
                principalColumn: "InventarioAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoId",
                table: "Alumbrado",
                column: "TareaTipoId",
                principalTable: "TareaTipoAlumbrado",
                principalColumn: "TareaTipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_EstatusAlumbrado_EstatusId",
                table: "Alumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_InventarioAlumbradoId",
                table: "Alumbrado");

            migrationBuilder.DropForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoId",
                table: "Alumbrado");

            migrationBuilder.RenameColumn(
                name: "TareaTipoId",
                table: "Alumbrado",
                newName: "tareaTipoId");

            migrationBuilder.RenameColumn(
                name: "InventarioAlumbradoId",
                table: "Alumbrado",
                newName: "inventarioAlumbradoId");

            migrationBuilder.RenameColumn(
                name: "ExpedienteAlumbradoId",
                table: "Alumbrado",
                newName: "expedienteAlumbradoId");

            migrationBuilder.RenameColumn(
                name: "EstatusId",
                table: "Alumbrado",
                newName: "estatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_TareaTipoId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_tareaTipoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_InventarioAlumbradoId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_inventarioAlumbradoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_ExpedienteAlumbradoId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_expedienteAlumbradoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alumbrado_EstatusId",
                table: "Alumbrado",
                newName: "IX_Alumbrado_estatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_EstatusAlumbrado_estatusId",
                table: "Alumbrado",
                column: "estatusId",
                principalTable: "EstatusAlumbrado",
                principalColumn: "EstatusAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_expedienteAlumbradoId",
                table: "Alumbrado",
                column: "expedienteAlumbradoId",
                principalTable: "ExpedienteAlumbrado",
                principalColumn: "ExpedienteAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_InventarioAlumbrado_inventarioAlumbradoId",
                table: "Alumbrado",
                column: "inventarioAlumbradoId",
                principalTable: "InventarioAlumbrado",
                principalColumn: "InventarioAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_TareaTipoAlumbrado_tareaTipoId",
                table: "Alumbrado",
                column: "tareaTipoId",
                principalTable: "TareaTipoAlumbrado",
                principalColumn: "TareaTipoId");
        }
    }
}
