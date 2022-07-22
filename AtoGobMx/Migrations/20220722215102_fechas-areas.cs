using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class fechasareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "FechaBaja",
                table: "Area");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Area",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
