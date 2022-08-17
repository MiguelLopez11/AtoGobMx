using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class expedientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Localidad",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Municipio",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "NumeroExterior",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "NumeroInterior",
                table: "Empleados");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            //migrationBuilder.CreateTable(
            //    name: "ExpedienteDigitales",
            //    columns: table => new
            //    {
            //        ExpedienteDigitalId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        EmpleadoId = table.Column<int>(type: "int", nullable: false),
            //        Estado = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Municipio = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Localidad = table.Column<string>(type: "longtext", nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Calle = table.Column<string>(type: "longtext", nullable: false)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        NumeroExterior = table.Column<int>(type: "int", nullable: false),
            //        NumeroInterior = table.Column<int>(type: "int", nullable: true),
            //        CodigoPostal = table.Column<int>(type: "int", nullable: false),
            //        CorreoElectronico = table.Column<string>(type: "longtext", nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        FechaIngreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            //        FechaBaja = table.Column<DateTime>(type: "datetime(6)", nullable: false),
            //        Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ExpedienteDigitales", x => x.ExpedienteDigitalId);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpedienteDigitales");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaAlta",
                table: "Empleados",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Empleados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CodigoPostal",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Empleados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Localidad",
                table: "Empleados",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Municipio",
                table: "Empleados",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "NumeroExterior",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroInterior",
                table: "Empleados",
                type: "int",
                nullable: true);
        }
    }
}
