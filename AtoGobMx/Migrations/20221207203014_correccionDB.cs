using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtoGobMx.Migrations
{
    public partial class correccionDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

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
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
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
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusAlumbrado", x => x.EstatusAlumbradoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstatusReceta",
                columns: table => new
                {
                    EstatusRecetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SERMED_EstatusRecetaEstatusRecetaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusReceta", x => x.EstatusRecetaId);
                    table.ForeignKey(
                        name: "FK_EstatusReceta_EstatusReceta_SERMED_EstatusRecetaEstatusRecet~",
                        column: x => x.SERMED_EstatusRecetaEstatusRecetaId,
                        principalTable: "EstatusReceta",
                        principalColumn: "EstatusRecetaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstatusVehiculo",
                columns: table => new
                {
                    EstatusVehiculoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusVehiculo", x => x.EstatusVehiculoId);
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
                name: "Medicamento",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contenido = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CantidadDisponible = table.Column<int>(type: "int", nullable: true),
                    CantidadFaltante = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.ProductoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OP_EstatusObras",
                columns: table => new
                {
                    EstatusObraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OP_EstatusObras", x => x.EstatusObraId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PROV_EstatusVale",
                columns: table => new
                {
                    EstatusValeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_EstatusVale", x => x.EstatusValeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PROV_Producto",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_Producto", x => x.ProductoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PROV_Proveedor",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RFC = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RepresentanteLegal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_Proveedor", x => x.ProveedorId);
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
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareaTipoAlumbrado", x => x.TareaTipoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoMobiliario",
                columns: table => new
                {
                    TipoMobiliarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMobiliario", x => x.TipoMobiliarioId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoVales",
                columns: table => new
                {
                    TipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVales", x => x.TipoId);
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
                name: "Cementerios",
                columns: table => new
                {
                    CementeriosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombrePropietario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroEspasios = table.Column<int>(type: "int", nullable: false),
                    MetrosCorrespondientes = table.Column<float>(type: "float", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    EspaciosDisponibles = table.Column<int>(type: "int", nullable: false),
                    DireccionId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cementerios", x => x.CementeriosId);
                    table.ForeignKey(
                        name: "FK_Cementerios_DireccionCementerio_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "DireccionCementerio",
                        principalColumn: "DireccionId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculo",
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
                    EstatusVehiculoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.VehiculoId);
                    table.ForeignKey(
                        name: "FK_Vehiculo_EstatusVehiculo_EstatusVehiculoId",
                        column: x => x.EstatusVehiculoId,
                        principalTable: "EstatusVehiculo",
                        principalColumn: "EstatusVehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ObrasPublicas",
                columns: table => new
                {
                    ObraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitud = table.Column<float>(type: "float", nullable: false),
                    Longitud = table.Column<float>(type: "float", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EstatusObraId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObrasPublicas", x => x.ObraId);
                    table.ForeignKey(
                        name: "FK_ObrasPublicas_OP_EstatusObras_EstatusObraId",
                        column: x => x.EstatusObraId,
                        principalTable: "OP_EstatusObras",
                        principalColumn: "EstatusObraId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alumbrado",
                columns: table => new
                {
                    AlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreObra = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                name: "Mobiliario",
                columns: table => new
                {
                    MobiliarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodigoInventario = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripción = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    TipoMobiliarioId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mobiliario", x => x.MobiliarioId);
                    table.ForeignKey(
                        name: "FK_Mobiliario_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mobiliario_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mobiliario_TipoMobiliario_TipoMobiliarioId",
                        column: x => x.TipoMobiliarioId,
                        principalTable: "TipoMobiliario",
                        principalColumn: "TipoMobiliarioId",
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
                    Nomenclatura = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LugarPublico = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localidad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AlumbradoId = table.Column<int>(type: "int", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    PAT_VehiculoVehiculoId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ExpedienteAlumbrado_Vehiculo_PAT_VehiculoVehiculoId",
                        column: x => x.PAT_VehiculoVehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Monitor",
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
                    table.PrimaryKey("PK_Monitor", x => x.MonitorId);
                    table.ForeignKey(
                        name: "FK_Monitor_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Mouse",
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
                    table.PrimaryKey("PK_Mouse", x => x.MouseId);
                    table.ForeignKey(
                        name: "FK_Mouse_EquipoComputo_EquipoComputoId",
                        column: x => x.EquipoComputoId,
                        principalTable: "EquipoComputo",
                        principalColumn: "EquipoComputoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teclado",
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
                    table.PrimaryKey("PK_Teclado", x => x.TecladoId);
                    table.ForeignKey(
                        name: "FK_Teclado_EquipoComputo_EquipoComputoId",
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
                name: "VehiculosAlumbrado",
                columns: table => new
                {
                    VehiculoAlumbradoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VehiculoId = table.Column<int>(type: "int", nullable: true),
                    ExpedienteAlumbradoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculosAlumbrado", x => x.VehiculoAlumbradoId);
                    table.ForeignKey(
                        name: "FK_VehiculosAlumbrado_ExpedienteAlumbrado_ExpedienteAlumbradoId",
                        column: x => x.ExpedienteAlumbradoId,
                        principalTable: "ExpedienteAlumbrado",
                        principalColumn: "ExpedienteAlumbradoId");
                    table.ForeignKey(
                        name: "FK_VehiculosAlumbrado_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "VehiculoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Armeria",
                columns: table => new
                {
                    ArmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nomenclatura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoArma = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calibre = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armeria", x => x.ArmaId);
                    table.ForeignKey(
                        name: "FK_Armeria_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    CitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Motivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaDesde = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaHasta = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.CitaId);
                    table.ForeignKey(
                        name: "FK_Cita_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ControlDeVales",
                columns: table => new
                {
                    ControlValeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaEmicion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    FechaVigencia = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    ProveedorId = table.Column<int>(type: "int", nullable: true),
                    EstatusValeId = table.Column<int>(type: "int", nullable: true),
                    TipoId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlDeVales", x => x.ControlValeId);
                    table.ForeignKey(
                        name: "FK_ControlDeVales_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                    table.ForeignKey(
                        name: "FK_ControlDeVales_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "DepartamentoId");
                    table.ForeignKey(
                        name: "FK_ControlDeVales_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_ControlDeVales_PROV_EstatusVale_EstatusValeId",
                        column: x => x.EstatusValeId,
                        principalTable: "PROV_EstatusVale",
                        principalColumn: "EstatusValeId");
                    table.ForeignKey(
                        name: "FK_ControlDeVales_PROV_Proveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "PROV_Proveedor",
                        principalColumn: "ProveedorId");
                    table.ForeignKey(
                        name: "FK_ControlDeVales_TipoVales_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoVales",
                        principalColumn: "TipoId");
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
                name: "ExpedienteMedico",
                columns: table => new
                {
                    ExpedienteMedicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Estatura = table.Column<float>(type: "float", nullable: true),
                    Peso = table.Column<float>(type: "float", nullable: true),
                    TipoSangre = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sexo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alergias = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discapacidad = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AntecedentesPersonales = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AntecedentesFamiliares = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpedienteMedico", x => x.ExpedienteMedicoId);
                    table.ForeignKey(
                        name: "FK_ExpedienteMedico_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OP_Empleados",
                columns: table => new
                {
                    EmpleadoObrasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    ObraId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OP_Empleados", x => x.EmpleadoObrasId);
                    table.ForeignKey(
                        name: "FK_OP_Empleados_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_OP_Empleados_ObrasPublicas_ObraId",
                        column: x => x.ObraId,
                        principalTable: "ObrasPublicas",
                        principalColumn: "ObraId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    RecetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    EstatusRecetaId = table.Column<int>(type: "int", nullable: true),
                    diagnostico = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaAlta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.RecetaId);
                    table.ForeignKey(
                        name: "FK_Receta_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId");
                    table.ForeignKey(
                        name: "FK_Receta_EstatusReceta_EstatusRecetaId",
                        column: x => x.EstatusRecetaId,
                        principalTable: "EstatusReceta",
                        principalColumn: "EstatusRecetaId");
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
                name: "PROV_DetalleVale",
                columns: table => new
                {
                    DetalleValeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    IVA = table.Column<float>(type: "float", nullable: true),
                    Total = table.Column<float>(type: "float", nullable: true),
                    ControlValeId = table.Column<int>(type: "int", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PROV_ProductoProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_DetalleVale", x => x.DetalleValeId);
                    table.ForeignKey(
                        name: "FK_PROV_DetalleVale_ControlDeVales_ControlValeId",
                        column: x => x.ControlValeId,
                        principalTable: "ControlDeVales",
                        principalColumn: "ControlValeId");
                    table.ForeignKey(
                        name: "FK_PROV_DetalleVale_PROV_Producto_PROV_ProductoProductoId",
                        column: x => x.PROV_ProductoProductoId,
                        principalTable: "PROV_Producto",
                        principalColumn: "ProductoId");
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

            migrationBuilder.CreateTable(
                name: "ProductoReceta",
                columns: table => new
                {
                    ProductoRecetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RecetaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoReceta", x => x.ProductoRecetaId);
                    table.ForeignKey(
                        name: "FK_ProductoReceta_Medicamento_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Medicamento",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoReceta_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Receta",
                        principalColumn: "RecetaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PROV_DetalleProducto",
                columns: table => new
                {
                    DetalleProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductoId = table.Column<int>(type: "int", nullable: true),
                    DetalleValeId = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<float>(type: "float", nullable: true),
                    Archivado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROV_DetalleProducto", x => x.DetalleProductoId);
                    table.ForeignKey(
                        name: "FK_PROV_DetalleProducto_PROV_DetalleVale_DetalleValeId",
                        column: x => x.DetalleValeId,
                        principalTable: "PROV_DetalleVale",
                        principalColumn: "DetalleValeId");
                    table.ForeignKey(
                        name: "FK_PROV_DetalleProducto_PROV_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "PROV_Producto",
                        principalColumn: "ProductoId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "DepartamentoId", "Archivado", "Descripcion", "Nombre" },
                values: new object[] { 1, false, null, "Direccion de Sistemas" });

            migrationBuilder.InsertData(
                table: "EstatusReceta",
                columns: new[] { "EstatusRecetaId", "Archivado", "Descripcion", "Nombre", "SERMED_EstatusRecetaEstatusRecetaId" },
                values: new object[,]
                {
                    { 1, false, null, "Pendiente", null },
                    { 2, false, null, "Surtido", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Archivado", "Descripcion", "Nombre" },
                values: new object[] { 1, false, null, "Administrador" });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "AreaId", "Archivado", "DepartamentoId", "Descripcion", "Nombre" },
                values: new object[] { 1, false, 1, null, "Soporte TI" });

            migrationBuilder.InsertData(
                table: "PuestoTrabajo",
                columns: new[] { "PuestoTrabajoId", "Archivado", "AreaId", "DepartamentoId", "Nombre" },
                values: new object[] { 1, false, 1, 1, "Auxiliar de soporte" });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "EmpleadoId", "Archivado", "AreaId", "DepartamentoId", "FechaAlta", "FechaBaja", "NombreCompleto", "PuestoTrabajoId", "TieneExpediente" },
                values: new object[] { 1, false, 1, 1, new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Local), null, "Administrador", 1, true });

            migrationBuilder.InsertData(
                table: "ExpedienteDigital",
                columns: new[] { "ExpedienteDigitalId", "Archivado", "Calle", "CodigoPostal", "CorreoElectronico", "EmpleadoId", "Estado", "FechaNacimiento", "Localidad", "Municipio", "NumeroExterior", "NumeroInterior" },
                values: new object[] { 1, false, null, null, null, 1, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "ExpedienteMedico",
                columns: new[] { "ExpedienteMedicoId", "Alergias", "AntecedentesFamiliares", "AntecedentesPersonales", "Archivado", "Discapacidad", "EmpleadoId", "Estatura", "Peso", "Sexo", "TipoSangre" },
                values: new object[] { 1, null, null, null, false, null, 1, null, null, null, null });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Archivado", "ConfirmarContraseña", "Contraseña", "EmpleadoId", "NombreUsuario", "RoleId" },
                values: new object[] { 1, false, "Admin123", "Admin123", 1, "Administrador", 1 });

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
                name: "IX_Armeria_EmpleadoId",
                table: "Armeria",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cementerios_DireccionId",
                table: "Cementerios",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EmpleadoId",
                table: "Cita",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_AreaId",
                table: "ControlDeVales",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_DepartamentoId",
                table: "ControlDeVales",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_EmpleadoId",
                table: "ControlDeVales",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_EstatusValeId",
                table: "ControlDeVales",
                column: "EstatusValeId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_ProveedorId",
                table: "ControlDeVales",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlDeVales_TipoId",
                table: "ControlDeVales",
                column: "TipoId");

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
                name: "IX_EstatusReceta_SERMED_EstatusRecetaEstatusRecetaId",
                table: "EstatusReceta",
                column: "SERMED_EstatusRecetaEstatusRecetaId");

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
                name: "IX_ExpedienteAlumbrado_PAT_VehiculoVehiculoId",
                table: "ExpedienteAlumbrado",
                column: "PAT_VehiculoVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteDigital_EmpleadoId",
                table: "ExpedienteDigital",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpedienteMedico_EmpleadoId",
                table: "ExpedienteMedico",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobiliario_AreaId",
                table: "Mobiliario",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobiliario_DepartamentoId",
                table: "Mobiliario",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mobiliario_TipoMobiliarioId",
                table: "Mobiliario",
                column: "TipoMobiliarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitor_EquipoComputoId",
                table: "Monitor",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouse_EquipoComputoId",
                table: "Mouse",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_ObrasPublicas_EstatusObraId",
                table: "ObrasPublicas",
                column: "EstatusObraId");

            migrationBuilder.CreateIndex(
                name: "IX_OP_Empleados_EmpleadoId",
                table: "OP_Empleados",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_OP_Empleados_ObraId",
                table: "OP_Empleados",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReceta_ProductoId",
                table: "ProductoReceta",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoReceta_RecetaId",
                table: "ProductoReceta",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleProducto_DetalleValeId",
                table: "PROV_DetalleProducto",
                column: "DetalleValeId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleProducto_ProductoId",
                table: "PROV_DetalleProducto",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleVale_ControlValeId",
                table: "PROV_DetalleVale",
                column: "ControlValeId");

            migrationBuilder.CreateIndex(
                name: "IX_PROV_DetalleVale_PROV_ProductoProductoId",
                table: "PROV_DetalleVale",
                column: "PROV_ProductoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_AreaId",
                table: "PuestoTrabajo",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_PuestoTrabajo_DepartamentoId",
                table: "PuestoTrabajo",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_EmpleadoId",
                table: "Receta",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_EstatusRecetaId",
                table: "Receta",
                column: "EstatusRecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Teclado_EquipoComputoId",
                table: "Teclado",
                column: "EquipoComputoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RoleId",
                table: "Usuarios",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_EstatusVehiculoId",
                table: "Vehiculo",
                column: "EstatusVehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosAlumbrado_ExpedienteAlumbradoId",
                table: "VehiculosAlumbrado",
                column: "ExpedienteAlumbradoId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculosAlumbrado_VehiculoId",
                table: "VehiculosAlumbrado",
                column: "VehiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "Armeria");

            migrationBuilder.DropTable(
                name: "Aseo");

            migrationBuilder.DropTable(
                name: "Cementerios");

            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "EmpleadosAlumbrado");

            migrationBuilder.DropTable(
                name: "ExpedienteMedico");

            migrationBuilder.DropTable(
                name: "Mobiliario");

            migrationBuilder.DropTable(
                name: "Monitor");

            migrationBuilder.DropTable(
                name: "Mouse");

            migrationBuilder.DropTable(
                name: "OP_Empleados");

            migrationBuilder.DropTable(
                name: "ProductoReceta");

            migrationBuilder.DropTable(
                name: "PROV_DetalleProducto");

            migrationBuilder.DropTable(
                name: "Ruta");

            migrationBuilder.DropTable(
                name: "Teclado");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "VehiculosAlumbrado");

            migrationBuilder.DropTable(
                name: "Zona");

            migrationBuilder.DropTable(
                name: "ExpedienteDigital");

            migrationBuilder.DropTable(
                name: "DireccionCementerio");

            migrationBuilder.DropTable(
                name: "TipoMobiliario");

            migrationBuilder.DropTable(
                name: "ObrasPublicas");

            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "PROV_DetalleVale");

            migrationBuilder.DropTable(
                name: "EquipoComputo");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ExpedienteAlumbrado");

            migrationBuilder.DropTable(
                name: "OP_EstatusObras");

            migrationBuilder.DropTable(
                name: "EstatusReceta");

            migrationBuilder.DropTable(
                name: "ControlDeVales");

            migrationBuilder.DropTable(
                name: "PROV_Producto");

            migrationBuilder.DropTable(
                name: "InventarioEstatus");

            migrationBuilder.DropTable(
                name: "Alumbrado");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "PROV_EstatusVale");

            migrationBuilder.DropTable(
                name: "PROV_Proveedor");

            migrationBuilder.DropTable(
                name: "TipoVales");

            migrationBuilder.DropTable(
                name: "EstatusAlumbrado");

            migrationBuilder.DropTable(
                name: "InventarioAlumbrado");

            migrationBuilder.DropTable(
                name: "TareaTipoAlumbrado");

            migrationBuilder.DropTable(
                name: "EstatusVehiculo");

            migrationBuilder.DropTable(
                name: "PuestoTrabajo");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
