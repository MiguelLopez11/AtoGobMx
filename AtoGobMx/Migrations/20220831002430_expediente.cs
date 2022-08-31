using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class expediente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_ExpedienteDigital_EmpleadoId",
            //    table: "ExpedienteDigital");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroExterior",
                table: "ExpedienteDigital",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Municipio",
                table: "ExpedienteDigital",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "ExpedienteDigital",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CodigoPostal",
                table: "ExpedienteDigital",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "ExpedienteDigital",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ExpedienteDigital_EmpleadoId",
            //    table: "ExpedienteDigital",
            //    column: "EmpleadoId",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital");

            migrationBuilder.AlterColumn<int>(
                name: "NumeroExterior",
                table: "ExpedienteDigital",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ExpedienteDigital",
                keyColumn: "Municipio",
                keyValue: null,
                column: "Municipio",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Municipio",
                table: "ExpedienteDigital",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ExpedienteDigital",
                keyColumn: "Estado",
                keyValue: null,
                column: "Estado",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "ExpedienteDigital",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CodigoPostal",
                table: "ExpedienteDigital",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ExpedienteDigital",
                keyColumn: "Calle",
                keyValue: null,
                column: "Calle",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Calle",
                table: "ExpedienteDigital",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId");
        }
    }
}
