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

            modelBuilder.Entity("AtoGobMx.Models.Areas", b =>
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

            modelBuilder.Entity("AtoGobMx.Models.empleados", b =>
                {
                    b.Property<int>("idEmpleado")
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

                    b.Property<int?>("AreaTrabajoId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("CURP")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.Property<string>("RFC")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("idEmpleado");

                    b.HasIndex("AreaTrabajoId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("AtoGobMx.Models.empleados", b =>
                {
                    b.HasOne("AtoGobMx.Models.Areas", "Areas")
                        .WithMany("Empleados")
                        .HasForeignKey("AreaTrabajoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Areas");
                });

            modelBuilder.Entity("AtoGobMx.Models.Areas", b =>
                {
                    b.Navigation("Empleados");
                });
#pragma warning restore 612, 618
        }
    }
}
