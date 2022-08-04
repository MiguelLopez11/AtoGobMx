﻿using AtoGobMx.Models;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Context
{
    public class AtoGobMxContext : DbContext
    {
        public AtoGobMxContext(DbContextOptions<AtoGobMxContext> options) : base(options) 
        { 
        }
        public DbSet<Area> Area { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<FallasAlumbradoPublico> FallasAlumbradoPublico { get; set; }
    }
}
