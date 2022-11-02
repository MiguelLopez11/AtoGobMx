﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class Creacion_Tablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aseo",
                columns: table => new
                {
                    AseoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreServicio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstablecimientoPublico = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Objetivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aseo", x => x.AseoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cementerios",
                columns: table => new
                {
                    CementeriosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePropietario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroEspasios = table.Column<int>(type: "int", nullable: false),
                    MetrosCorrespondientes = table.Column<int>(type: "int", nullable: false),
                    EspaciosDisponibles = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cementerios", x => x.CementeriosId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DireccionCementerio",
                columns: table => new
                {
                    DireccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCementerio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Municipio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localidad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroExterior = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionCementerio", x => x.DireccionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstatusAlumbrado",
                columns: table => new
                {
                    EstatusAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreEstatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusAlumbrado", x => x.EstatusAlumbradoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventarioAlumbrado",
                columns: table => new
                {
                    InventarioAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioAlumbrado", x => x.InventarioAlumbradoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InventarioEstatus",
                columns: table => new
                {
                    EstatusEquipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioEstatus", x => x.EstatusEquipoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Vehiculo",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Color = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Placa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroSerie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AñoLanzamiento = table.Column<int>(type: "int", nullable: false),
                    Puertas = table.Column<int>(type: "int", nullable: false),
                    Transmisión = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Vehiculo", x => x.VehiculoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ruta",
                columns: table => new
                {
                    RutaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Origen = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Destino = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Obsevacion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruta", x => x.RutaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TareaTipoAlumbrado",
                columns: table => new
                {
                    TareaTipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTarea = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaTipoAlumbrado", x => x.TareaTipoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Zona",
                columns: table => new
                {
                    zonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zona", x => x.zonaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                    table.ForeignKey(
                        name: "FK_Area_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alumbrado",
                columns: table => new
                {
                    AlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DescripcionProblema = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescripcionDomicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TieneExpediente = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EstatusId = table.Column<int>(type: "int", nullable: true),
                    InventarioAlumbradoId = table.Column<int>(type: "int", nullable: true),
                    TareaTipoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumbrado", x => x.AlumbradoId);
                    table.ForeignKey(
                        name: "FK_Alumbrado_EstatusAlumbrado_EstatusId",
                        column: x => x.EstatusId,
                        principalTable: "EstatusAlumbrado",
                        principalColumn: "EstatusAlumbradoId");
                    table.ForeignKey(
                        name: "FK_Alumbrado_InventarioAlumbrado_InventarioAlumbradoId",
                        column: x => x.InventarioAlumbradoId,
                        principalTable: "InventarioAlumbrado",
                        principalColumn: "InventarioAlumbradoId");
                    table.ForeignKey(
                        name: "FK_Alumbrado_TareaTipoAlumbrado_TareaTipoId",
                        column: x => x.TareaTipoId,
                        principalTable: "TareaTipoAlumbrado",
                        principalColumn: "TareaTipoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipoComputo",
                columns: table => new
                {
                    EquipoComputoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoInventario = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemoriaRAM = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Almacenamiento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Procesador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    EstatusEquipoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoComputo", x => x.EquipoComputoId);
                    table.ForeignKey(
                        name: "FK_EquipoComputo_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoComputo_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoComputo_InventarioEstatus_EstatusEquipoId",
                        column: x => x.EstatusEquipoId,
                        principalTable: "InventarioEstatus",
                        principalColumn: "EstatusEquipoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Mobiliario",
                columns: table => new
                {
                    MobiliarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreMobiliario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripción = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Mobiliario", x => x.MobiliarioId);
                    table.ForeignKey(
                        name: "FK_PAT_Mobiliario_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PuestoTrabajo",
                columns: table => new
                {
                    PuestoTrabajoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestoTrabajo", x => x.PuestoTrabajoId);
                    table.ForeignKey(
                        name: "FK_PuestoTrabajo_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                    table.ForeignKey(
                        name: "FK_PuestoTrabajo_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExpedienteAlumbrado",
                columns: table => new
                {
                    ExpedienteAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DescripcionSolucion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LugarPublico = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localidad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AlumbradoId = table.Column<int>(type: "int", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteAlumbrado", x => x.ExpedienteAlumbradoId);
                    table.ForeignKey(
                        name: "FK_ExpedienteAlumbrado_Alumbrado_AlumbradoId",
                        column: x => x.AlumbradoId,
                        principalTable: "Alumbrado",
                        principalColumn: "AlumbradoId");
                    table.ForeignKey(
                        name: "FK_ExpedienteAlumbrado_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                    table.ForeignKey(
                        name: "FK_ExpedienteAlumbrado_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Monitor",
                columns: table => new
                {
                    MonitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pulgadas = table.Column<int>(type: "int", nullable: true),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Monitor", x => x.MonitorId);
                    table.ForeignKey(
                        name: "FK_PAT_Monitor_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Mouse",
                columns: table => new
                {
                    MouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConexion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Mouse", x => x.MouseId);
                    table.ForeignKey(
                        name: "FK_PAT_Mouse_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PAT_Teclado",
                columns: table => new
                {
                    TecladoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Marca = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConexion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EquipoComputoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAT_Teclado", x => x.TecladoId);
                    table.ForeignKey(
                        name: "FK_PAT_Teclado_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCompleto = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TieneExpediente = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    PuestoTrabajoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmpleadoId);
                    table.ForeignKey(
                        name: "FK_Empleados_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                    table.ForeignKey(
                        name: "FK_Empleados_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId");
                    table.ForeignKey(
                        name: "FK_Empleados_PuestoTrabajo_PuestoTrabajoId",
                        column: x => x.PuestoTrabajoId,
                        principalTable: "PuestoTrabajo",
                        principalColumn: "PuestoTrabajoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmpleadosAlumbrado",
                columns: table => new
                {
                    AlumbradoEmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    ExpedienteAlumbradoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadosAlumbrado", x => x.AlumbradoEmpleadoId);
                    table.ForeignKey(
                        name: "FK_EmpleadosAlumbrado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_EmpleadosAlumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                        column: x => x.ExpedienteAlumbradoId,
                        principalTable: "ExpedienteAlumbrado",
                        principalColumn: "ExpedienteAlumbradoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExpedienteDigital",
                columns: table => new
                {
                    ExpedienteDigitalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Estado = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Municipio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localidad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroExterior = table.Column<int>(type: "int", nullable: true),
                    NumeroInterior = table.Column<int>(type: "int", nullable: true),
                    CodigoPostal = table.Column<int>(type: "int", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteDigital", x => x.ExpedienteDigitalId);
                    table.ForeignKey(
                        name: "FK_ExpedienteDigital_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreUsuario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contraseña = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConfirmarContraseña = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Archivos",
                columns: table => new
                {
                    ArchivoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoArchivo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExpedienteDigitalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos", x => x.ArchivoId);
                    table.ForeignKey(
                        name: "FK_Archivos_ExpedienteDigital_ExpedienteDigitalId",
                        column: x => x.ExpedienteDigitalId,
                        principalTable: "ExpedienteDigital",
                        principalColumn: "ExpedienteDigitalId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_EstatusId",
                table: "Alumbrado",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_InventarioAlumbradoId",
                table: "Alumbrado",
                column: "InventarioAlumbradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumbrado_TareaTipoId",
                table: "Alumbrado",
                column: "TareaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_ExpedienteDigitalId",
                table: "Archivos",
                column: "ExpedienteDigitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_DepartamentoId",
                table: "Area",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_AreaId",
                table: "Empleados",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_DepartamentoId",
                table: "Empleados",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_PuestoTrabajoId",
                table: "Empleados",
                column: "PuestoTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadosAlumbrado_EmpleadoId",
                table: "EmpleadosAlumbrado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadosAlumbrado_ExpedienteAlumbradoId",
                table: "EmpleadosAlumbrado",
                column: "ExpedienteAlumbradoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_AreaId",
                table: "EquipoComputo",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_DepartamentoId",
                table: "EquipoComputo",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoComputo_EstatusEquipoId",
                table: "EquipoComputo",
                column: "EstatusEquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_AlumbradoId",
                table: "ExpedienteAlumbrado",
                column: "AlumbradoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_AreaId",
                table: "ExpedienteAlumbrado",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteAlumbrado_DepartamentoId",
                table: "ExpedienteAlumbrado",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Mobiliario_AreaId",
                table: "PAT_Mobiliario",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Monitor_EquipoComputoId",
                table: "PAT_Monitor",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Mouse_EquipoComputoId",
                table: "PAT_Mouse",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_PAT_Teclado_EquipoComputoId",
                table: "PAT_Teclado",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_AreaId",
                table: "PuestoTrabajo",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_DepartamentoId",
                table: "PuestoTrabajo",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RoleId",
                table: "Usuarios",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "Aseo");

            migrationBuilder.DropTable(
                name: "Cementerios");

            migrationBuilder.DropTable(
                name: "DireccionCementerio");

            migrationBuilder.DropTable(
                name: "EmpleadosAlumbrado");

            migrationBuilder.DropTable(
                name: "PAT_Mobiliario");

            migrationBuilder.DropTable(
                name: "PAT_Monitor");

            migrationBuilder.DropTable(
                name: "PAT_Mouse");

            migrationBuilder.DropTable(
                name: "PAT_Teclado");

            migrationBuilder.DropTable(
                name: "PAT_Vehiculo");

            migrationBuilder.DropTable(
                name: "Ruta");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Zona");

            migrationBuilder.DropTable(
                name: "ExpedienteDigital");

            migrationBuilder.DropTable(
                name: "ExpedienteAlumbrado");

            migrationBuilder.DropTable(
                name: "EquipoComputo");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Alumbrado");

            migrationBuilder.DropTable(
                name: "InventarioEstatus");

            migrationBuilder.DropTable(
                name: "PuestoTrabajo");

            migrationBuilder.DropTable(
                name: "EstatusAlumbrado");

            migrationBuilder.DropTable(
                name: "InventarioAlumbrado");

            migrationBuilder.DropTable(
                name: "TareaTipoAlumbrado");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}