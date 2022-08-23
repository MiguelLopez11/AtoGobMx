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
    [Migration("20220818225839_repararRelacionExpediente")]
    partial class repararRelacionExpediente
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AtoGobMx.Models.Archivos", b =>
                {
                    b.Property<int>("ArchivoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("DataFiles")
                        .HasColumnType("longblob");

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

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AreaId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApellidoMaterno")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaAlta")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaBaja")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaNacimiento")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("EmpleadoId");

                    b.HasIndex("AreaId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteDigital", b =>
                {
                    b.Property<int>("ExpedienteDigitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Calle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("CodigoPostal")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("CorreoElectronico")
                        .HasColumnType("longtext");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Localidad")
                        .HasColumnType("longtext");

                    b.Property<string>("Municipio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("NumeroExterior")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("NumeroInterior")
                        .HasColumnType("int");

                    b.HasKey("ExpedienteDigitalId");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("ExpedienteDigital");
                });

            modelBuilder.Entity("AtoGobMx.Models.FallasAlumbradoPublico", b =>
                {
                    b.Property<int>("FallaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DescripcionDomicilio")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NombreFalla")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("FallaId");

                    b.ToTable("FallasAlumbradoPublico");
                });

            modelBuilder.Entity("AtoGobMx.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Archivado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
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

            modelBuilder.Entity("AtoGobMx.Models.Archivos", b =>
                {
                    b.HasOne("AtoGobMx.Models.ExpedienteDigital", "expedienteDigital")
                        .WithMany()
                        .HasForeignKey("ExpedienteDigitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("expedienteDigital");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.HasOne("AtoGobMx.Models.Area", "Area")
                        .WithMany("Empleados")
                        .HasForeignKey("AreaId");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteDigital", b =>
                {
                    b.HasOne("AtoGobMx.Models.Empleado", "empleado")
                        .WithMany("ExpedientesDigitales")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("empleado");
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

            modelBuilder.Entity("AtoGobMx.Models.Area", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.Navigation("ExpedientesDigitales");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("AtoGobMx.Models.Role", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
