﻿// <auto-generated />
using System;
using AtoGobMx.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AtoGobMx.Migrations
{
    [DbContext(typeof(AtoGobMxContext))]
    partial class AtoGobMxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("FechaAlta")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaBaja")
                        .IsRequired()
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

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteEmpleado", b =>
                {
                    b.Property<int>("ExpedienteEmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CURP")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<string>("RFC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ExpedienteEmpleadoId");

                    b.HasIndex("EmpleadoId");

                    b.ToTable("ExpedienteEmpleado");
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
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.HasIndex("RoleId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.HasOne("AtoGobMx.Models.Area", "Area")
                        .WithMany("Empleados")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("AtoGobMx.Models.ExpedienteEmpleado", b =>
                {
                    b.HasOne("AtoGobMx.Models.Empleado", "Empleado")
                        .WithMany("ExpedienteEmpleados")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("AtoGobMx.Models.Usuario", b =>
                {
                    b.HasOne("AtoGobMx.Models.Role", "Role")
                        .WithMany("Usuarios")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("AtoGobMx.Models.Area", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.Empleado", b =>
                {
                    b.Navigation("ExpedienteEmpleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.Role", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
