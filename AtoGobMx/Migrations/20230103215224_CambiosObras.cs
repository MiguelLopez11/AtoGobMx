﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class CambiosObras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Longitud",
                table: "ObrasPublicas",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitud",
                table: "ObrasPublicas",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Longitud",
                table: "ObrasPublicas",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Latitud",
                table: "ObrasPublicas",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
