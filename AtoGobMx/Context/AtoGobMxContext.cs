using AtoGobMx.Models;
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
        public DbSet<ExpedienteFallas> ExpedienteFalla { get; set; }
        public DbSet<ExpedienteDigital> ExpedienteDigital { get; set; }
        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<PuestoTrabajo> PuestoTrabajo { get; set; }
    }
}
