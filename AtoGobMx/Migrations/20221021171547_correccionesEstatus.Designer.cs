﻿// <auto-generated />
using System;
using AtoGobMx.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AtoGobMx.Migrations
{
    [DbContext(typeof(AtoGobMxContext))]
    [Migration("20221021171547_correccionesEstatus")]
    partial class correccionesEstatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AtoGobMx.Models.Alumbrado", b =>
                {
                    b.Property<int>("AlumbradoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("DescripcionDomicilio")
                        .HasColumnType("longtext");

                    b.Property<string>("DescripcionProblema")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("EstatusId")
                        .HasColumnType("int");

                    b.Property<int?>("InventarioAlumbradoId")
                        .HasColumnType("int");

                    b.Property<int?>("TareaTipoId")
                        .HasColumnType("int");

                    b.HasKey("AlumbradoId");

                    b.HasIndex("EstatusId");

                    b.HasIndex("InventarioAlumbradoId");

                    b.HasIndex("TareaTipoId");

                    b.ToTable("Alumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Archivos", b =>
                {
                    b.Property<int>("ArchivoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ExpedienteDigitalId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TipoArchivo")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ArchivoId");

                    b.HasIndex("ExpedienteDigitalId");

                    b.ToTable("Archivos");
                });

            modelBuilder.Entity("AtoGobMx.Models.Area", b =>
                {
                    b.Property<int>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AreaId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("AtoGobMx.Models.Aseo", b =>
                {
                    b.Property<int>("AseoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EstablecimientoPublico")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreServicio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Objetivo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("RutaId")
                        .HasColumnType("int");

                    b.HasKey("AseoId");

                    b.ToTable("Aseo");
                });

            modelBuilder.Entity("AtoGobMx.Models.CategoriaInventario", b =>
                {
                    b.Property<int>("CategoriaInventarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreCategoria")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CategoriaInventarioId");

                    b.ToTable("CategoriaInventario");
                });

            modelBuilder.Entity("AtoGobMx.Models.Cementerios", b =>
                {
                    b.Property<int>("CementeriosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("EspaciosDisponibles")
                        .HasColumnType("int");

                    b.Property<int>("MetrosCorrespondientes")
                        .HasColumnType("int");

                    b.Property<string>("NombrePropietario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroEspasios")
                        .HasColumnType("int");

                    b.HasKey("CementeriosId");

                    b.ToTable("Cementerios");
                });

            modelBuilder.Entity("AtoGobMx.Models.Departamentos", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("DepartamentoId");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("AtoGobMx.Models.DireccionCementerio", b =>
                {
                    b.Property<int>("DireccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Localidad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Municipio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreCementerio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroExterior")
                        .HasColumnType("int");

                    b.HasKey("DireccionId");

                    b.ToTable("DireccionCementerio");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaAlta")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaBaja")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("PuestoTrabajoId")
                        .HasColumnType("int");

                    b.Property<bool>("TieneExpediente")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("EmpleadoId");

                    b.HasIndex("AreaId");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("PuestoTrabajoId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.EmpleadosAlumbrado", b =>
                {
                    b.Property<int>("AlumbradoEmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int?>("ExpedienteAlumbradoId")
                        .HasColumnType("int");

                    b.HasKey("AlumbradoEmpleadoId");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("ExpedienteAlumbradoId");

                    b.ToTable("EmpleadosAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.EstatusAlumbrado", b =>
                {
                    b.Property<int>("EstatusAlumbradoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreEstatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("EstatusAlumbradoId");

                    b.ToTable("EstatusAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteAlumbrado", b =>
                {
                    b.Property<int>("ExpedienteAlumbradoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AlumbradoId")
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("DescripcionSolucion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Localidad")
                        .HasColumnType("longtext");

                    b.Property<string>("LugarPublico")
                        .HasColumnType("longtext");

                    b.HasKey("ExpedienteAlumbradoId");

                    b.HasIndex("AlumbradoId");

                    b.HasIndex("AreaId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("ExpedienteAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteDigital", b =>
                {
                    b.Property<int>("ExpedienteDigitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Calle")
                        .HasColumnType("longtext");

                    b.Property<int?>("CodigoPostal")
                        .HasColumnType("int");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("longtext");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Localidad")
                        .HasColumnType("longtext");

                    b.Property<string>("Municipio")
                        .HasColumnType("longtext");

                    b.Property<int?>("NumeroExterior")
                        .HasColumnType("int");

                    b.Property<int?>("NumeroInterior")
                        .HasColumnType("int");

                    b.HasKey("ExpedienteDigitalId");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("ExpedienteDigital");
                });

            modelBuilder.Entity("AtoGobMx.Models.Inventario", b =>
                {
                    b.Property<int>("InventarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("CategoriaInventarioId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Dependencia")
                        .HasColumnType("longtext");

                    b.Property<string>("Descripción")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Folio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("InventarioEstatusId")
                        .HasColumnType("int");

                    b.Property<bool?>("isOnDependency")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("InventarioId");

                    b.HasIndex("CategoriaInventarioId");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("InventarioEstatusId");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("AtoGobMx.Models.InventarioAlumbrado", b =>
                {
                    b.Property<int>("InventarioAlumbradoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("InventarioAlumbradoId");

                    b.ToTable("InventarioAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.InventarioEstatus", b =>
                {
                    b.Property<int>("InventarioEstatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("InventarioEstatusId");

                    b.ToTable("InventarioEstatus");
                });

            modelBuilder.Entity("AtoGobMx.Models.PuestoTrabajo", b =>
                {
                    b.Property<int>("PuestoTrabajoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.HasKey("PuestoTrabajoId");

                    b.HasIndex("AreaId");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("PuestoTrabajo");
                });

            modelBuilder.Entity("AtoGobMx.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AtoGobMx.Models.Ruta", b =>
                {
                    b.Property<int>("RutaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Destino")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Obsevacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Origen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RutaId");

                    b.ToTable("Ruta");
                });

            modelBuilder.Entity("AtoGobMx.Models.TareaTipoAlumbrado", b =>
                {
                    b.Property<int>("TareaTipoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreTarea")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TareaTipoId");

                    b.ToTable("TareaTipoAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ConfirmarContraseña")
                        .HasColumnType("longtext");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("RoleId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AtoGobMx.Models.Alumbrado", b =>
                {
                    b.HasOne("AtoGobMx.Models.EstatusAlumbrado", "Estatus")
                        .WithMany("Alumbrado")
                        .HasForeignKey("EstatusId");

                    b.HasOne("AtoGobMx.Models.InventarioAlumbrado", "InventarioAlumbrado")
                        .WithMany("Alumbrado")
                        .HasForeignKey("InventarioAlumbradoId");

                    b.HasOne("AtoGobMx.Models.TareaTipoAlumbrado", "TareaTipoAlumbrado")
                        .WithMany("Alumbrado")
                        .HasForeignKey("TareaTipoId");

                    b.Navigation("Estatus");

                    b.Navigation("InventarioAlumbrado");

                    b.Navigation("TareaTipoAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Archivos", b =>
                {
                    b.HasOne("AtoGobMx.Models.ExpedienteDigital", "expedienteDigital")
                        .WithMany("Archivos")
                        .HasForeignKey("ExpedienteDigitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("expedienteDigital");
                });

            modelBuilder.Entity("AtoGobMx.Models.Area", b =>
                {
                    b.HasOne("AtoGobMx.Models.Departamentos", "Departamentos")
                        .WithMany()
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamentos");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.HasOne("AtoGobMx.Models.Area", "Area")
                        .WithMany("Empleados")
                        .HasForeignKey("AreaId");

                    b.HasOne("AtoGobMx.Models.Departamentos", "Departamentos")
                        .WithMany()
                        .HasForeignKey("DepartamentoId");

                    b.HasOne("AtoGobMx.Models.PuestoTrabajo", "PuestoTrabajo")
                        .WithMany()
                        .HasForeignKey("PuestoTrabajoId");

                    b.Navigation("Area");

                    b.Navigation("Departamentos");

                    b.Navigation("PuestoTrabajo");
                });

            modelBuilder.Entity("AtoGobMx.Models.EmpleadosAlumbrado", b =>
                {
                    b.HasOne("AtoGobMx.Models.Empleado", "Empleados")
                        .WithMany("EmpleadosAlumbrado")
                        .HasForeignKey("EmpleadoId");

                    b.HasOne("AtoGobMx.Models.ExpedienteAlumbrado", "ExpedienteAlumbrado")
                        .WithMany("EmpleadosAlumbrado")
                        .HasForeignKey("ExpedienteAlumbradoId");

                    b.Navigation("Empleados");

                    b.Navigation("ExpedienteAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteAlumbrado", b =>
                {
                    b.HasOne("AtoGobMx.Models.Alumbrado", "Alumbrado")
                        .WithMany("ExpedienteAlumbrado")
                        .HasForeignKey("AlumbradoId");

                    b.HasOne("AtoGobMx.Models.Area", "Area")
                        .WithMany("ExpedienteAlumbrado")
                        .HasForeignKey("AreaId");

                    b.HasOne("AtoGobMx.Models.Departamentos", "Departamentos")
                        .WithMany("ExpedienteAlumbrado")
                        .HasForeignKey("DepartamentoId");

                    b.Navigation("Alumbrado");

                    b.Navigation("Area");

                    b.Navigation("Departamentos");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteDigital", b =>
                {
                    b.HasOne("AtoGobMx.Models.Empleado", "Empleados")
                        .WithMany()
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.Inventario", b =>
                {
                    b.HasOne("AtoGobMx.Models.CategoriaInventario", "CategoriaInventario")
                        .WithMany()
                        .HasForeignKey("CategoriaInventarioId");

                    b.HasOne("AtoGobMx.Models.Departamentos", "Departamentos")
                        .WithMany("Inventario")
                        .HasForeignKey("DepartamentoId");

                    b.HasOne("AtoGobMx.Models.InventarioEstatus", "InventarioEstatus")
                        .WithMany()
                        .HasForeignKey("InventarioEstatusId");

                    b.Navigation("CategoriaInventario");

                    b.Navigation("Departamentos");

                    b.Navigation("InventarioEstatus");
                });

            modelBuilder.Entity("AtoGobMx.Models.PuestoTrabajo", b =>
                {
                    b.HasOne("AtoGobMx.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("AtoGobMx.Models.Departamentos", "Departamentos")
                        .WithMany()
                        .HasForeignKey("DepartamentoId");

                    b.Navigation("Area");

                    b.Navigation("Departamentos");
                });

            modelBuilder.Entity("AtoGobMx.Models.Usuario", b =>
                {
                    b.HasOne("AtoGobMx.Models.Empleado", "Empleado")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AtoGobMx.Models.Role", "Role")
                        .WithMany("Usuarios")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AtoGobMx.Models.Alumbrado", b =>
                {
                    b.Navigation("ExpedienteAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Area", b =>
                {
                    b.Navigation("Empleados");

                    b.Navigation("ExpedienteAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Departamentos", b =>
                {
                    b.Navigation("ExpedienteAlumbrado");

                    b.Navigation("Inventario");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.Navigation("EmpleadosAlumbrado");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("AtoGobMx.Models.EstatusAlumbrado", b =>
                {
                    b.Navigation("Alumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteAlumbrado", b =>
                {
                    b.Navigation("EmpleadosAlumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteDigital", b =>
                {
                    b.Navigation("Archivos");
                });

            modelBuilder.Entity("AtoGobMx.Models.InventarioAlumbrado", b =>
                {
                    b.Navigation("Alumbrado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Role", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("AtoGobMx.Models.TareaTipoAlumbrado", b =>
                {
                    b.Navigation("Alumbrado");
                });
#pragma warning restore 612, 618
        }
    }
}
