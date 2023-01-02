using AtoGobMx.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Context
{
    public class AtoGobMxContext :  IdentityDbContext<IdentityUser>
    {
        public AtoGobMxContext(DbContextOptions<AtoGobMxContext> options) : base(options)
        {
        }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Alumbrado> Alumbrado { get; set; }
        public DbSet<ArchivosAlumbrado> ArchivosAlumbrado  { get; set; }
        public DbSet<ArchivosCementerios> ArchivosCementerios { get; set; }
        public DbSet<ArchivosAseo> ArchivosAseo { get; set; }
        public DbSet<ArchivosObras> ArchivosObras { get; set; }
        public DbSet<ExpedienteAlumbrado> ExpedienteAlumbrado { get; set; }
        public DbSet<EstatusAlumbrado> EstatusAlumbrado { get; set; }
        public DbSet<TareaTipoAlumbrado> TareaTipoAlumbrado { get; set; }
        public DbSet<EmpleadosAlumbrado> EmpleadosAlumbrado { get; set; }
        public DbSet<VehiculosAlumbrado> VehiculosAlumbrado { get; set; }
        public DbSet<Aseo> Aseo { get; set; }
        public DbSet<AseoVehiculo> AseoVehiculo { get; set; }
        public DbSet<AseoEmpleado> AseoEmpleado { get; set; }
        public DbSet<Ruta> Ruta { get; set; }
        public DbSet<Coordenadas_Rutas> Coordenadas_Ruta { get; set; }
        public DbSet<Zona> Zona { get; set; }
        public DbSet<Gabetas> Gabetas { get; set; }
        public DbSet<Cementerio> Cementerio { get; set; }
        public DbSet<ExpedienteDigital> ExpedienteDigital { get; set; }
        public DbSet<OP_Empleados> OP_Empleados { get; set; }
        public DbSet<OP_Vehiculos> OP_Vehiculos { get; set; }
        public DbSet<OP_Obras> ObrasPublicas { get; set; }
        public DbSet<OP_EstatusObras> OP_EstatusObras { get; set; }
        public DbSet<PROV_ControlVales> ControlDeVales { get; set; }
        public DbSet<ExpedienteVale> ExpedienteVale { get; set; }
        public DbSet<PROV_Vehiculo> PROV_Vehiculo { get; set; }
        public DbSet<PROV_EstatusVale> PROV_EstatusVale { get; set; }
        public DbSet<PROV_TipoVales> TipoVales { get; set; }
        public DbSet<PROV_DetalleVale> PROV_DetalleVale { get; set; }
        public DbSet<PROV_Producto> PROV_Producto { get; set; }
        public DbSet<PROV_Proveedor> PROV_Proveedor { get; set; }
        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<ArchivosEquipoComputo> ArchivosEquipoComputos { get; set; }
        public DbSet<ArchivosVehiculos> archivosVehiculos { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<PuestoTrabajo> PuestoTrabajo { get; set; }
        public DbSet<PAT_EquipoComputo> EquipoComputo { get; set; }
        public DbSet<PAT_Vehiculo> Vehiculo { get; set; }
        public DbSet<PAT_Mobiliario> Mobiliario { get; set; }
        public DbSet<PAT_TipoMobiliario> TipoMobiliario { get; set; }
        public DbSet<PAT_EstatusEquipo> InventarioEstatus { get; set; }
        public DbSet<PAT_EstatusVehiculo> EstatusVehiculo { get; set; }
        public DbSet<PAT_Armeria> Armeria { get; set; }
        public DbSet<SERMED_Cita> Cita { get; set; }
        public DbSet<SERMED_Producto> Medicamento { get; set; }
        public DbSet<SERMED_ExpedienteMedico> ExpedienteMedico { get; set; }
        public DbSet<SERMED_ProductosReceta> ProductoReceta { get; set; }
        public DbSet<SERMED_Receta> Receta { get; set; }
        public DbSet<SERMED_EstatusReceta> EstatusReceta { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Departamentos>().HasData(new Departamentos { DepartamentoId = 1, Nombre = "Direccion de Sistemas", Descripcion = null, Archivado = false });
            modelBuilder.Entity<PuestoTrabajo>().HasData(new PuestoTrabajo { PuestoTrabajoId = 1, Nombre = "Administrador", DepartamentoId = 1, Archivado = false });
            modelBuilder.Entity<SERMED_EstatusReceta>().HasData(new SERMED_EstatusReceta { EstatusRecetaId = 1, Nombre = "Pendiente", Descripcion = null, Archivado = false });
            modelBuilder.Entity<SERMED_EstatusReceta>().HasData(new SERMED_EstatusReceta { EstatusRecetaId = 2, Nombre = "Surtido", Descripcion = null, Archivado = false });
        }
    }
}
