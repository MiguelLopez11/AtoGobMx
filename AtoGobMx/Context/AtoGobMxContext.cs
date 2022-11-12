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
        public DbSet<Alumbrado> Alumbrado { get; set; }
        public DbSet<ExpedienteAlumbrado> ExpedienteAlumbrado { get; set; }
        public DbSet<EstatusAlumbrado> EstatusAlumbrado { get; set; }
        public DbSet<TareaTipoAlumbrado> TareaTipoAlumbrado { get; set; }
        public DbSet<InventarioAlumbrado> InventarioAlumbrado { get; set; }
        public DbSet<EmpleadosAlumbrado> EmpleadosAlumbrado { get; set; }
        public DbSet<Aseo> Aseo { get; set; }
        public DbSet<Ruta> Ruta { get; set; }
        public DbSet<Zona> Zona { get; set; }
        public DbSet<Cementerios> Cementerios { get; set; }
        public DbSet<DireccionCementerio> DireccionCementerio { get; set; }
        public DbSet<ExpedienteDigital> ExpedienteDigital { get; set; }
        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<PuestoTrabajo> PuestoTrabajo { get; set; }
        public DbSet<PAT_EquipoComputo> EquipoComputo { get; set; }
        public DbSet<PAT_Monitor> Monitor { get; set; }
        public DbSet<PAT_Mouse> Mouse { get; set; }
        public DbSet<PAT_Teclado> Teclado { get; set; }
        public DbSet<PAT_Vehiculo> Vehiculo { get; set; }
        public DbSet<PAT_Mobiliario> Mobiliario { get; set; }
        public DbSet<PAT_TipoMobiliario> TipoMobiliario { get; set; }
        public DbSet<PAT_EstatusEquipo> InventarioEstatus { get; set; }
        public DbSet<PAT_EstatusVehiculo> EstatusVehiculo { get; set; }
        public DbSet<PAT_Armeria> Armeria { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Departamentos>().HasData(new Departamentos {DepartamentoId = 1, Nombre = "Direccion de Sistemas", Descripcion = null, Archivado = false });          
            modelBuilder.Entity<Area>().HasData(new Area {AreaId = 1, Nombre = "Soporte TI", Descripcion = null, DepartamentoId = 1, Archivado = false });          
            modelBuilder.Entity<PuestoTrabajo>().HasData(new PuestoTrabajo { PuestoTrabajoId = 1, Nombre = "Auxiliar de soporte", DepartamentoId = 1,AreaId = 1, Archivado = false });          
            modelBuilder.Entity<Empleado>().HasData(new Empleado { EmpleadoId = 1, NombreCompleto = "Administrador", DepartamentoId = 1,AreaId = 1,PuestoTrabajoId = 1, TieneExpediente = true, FechaAlta = DateTime.Today, FechaBaja = null, Archivado = false });          
            modelBuilder.Entity<Role>().HasData(new Role { RoleId = 1, Nombre = "Administrador", Descripcion = null, Archivado = false });          
            modelBuilder.Entity<Usuario>().HasData(new Usuario { UsuarioId = 1, NombreUsuario = "Administrador",Contraseña="Admin123", ConfirmarContraseña="Admin123",EmpleadoId = 1,RoleId = 1, Archivado = false });          
        }
    }

}
