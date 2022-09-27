using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class ExpedienteAlumbrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Alumbrado_ExpedienteAlumbrado_expedienteAlumbradoId",
            //    table: "Alumbrado");

            //migrationBuilder.DropTable(
            //    name: "ExpedienteAlumbrado");

            //migrationBuilder.DropIndex(
            //    name: "IX_Alumbrado_expedienteAlumbradoId",
            //    table: "Alumbrado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpedienteAlumbrado",
                columns: table => new
                {
                    ExpedienteAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DescripcionDomicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescripcionSolucion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Tarea = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteAlumbrado", x => x.ExpedienteAlumbradoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_expedienteAlumbradoId",
                table: "Alumbrado",
                column: "expedienteAlumbradoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumbrado_ExpedienteAlumbrado_expedienteAlumbradoId",
                table: "Alumbrado",
                column: "expedienteAlumbradoId",
                principalTable: "ExpedienteAlumbrado",
                principalColumn: "ExpedienteAlumbradoId");
        }
    }
}
