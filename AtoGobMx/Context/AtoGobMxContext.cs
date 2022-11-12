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
        public DbSet<OP_Obras> ObrasPublicas { get; set; }
        public DbSet<OP_EstatusObras> OP_EstatusObras { get; set; }
        public DbSet<PROV_ControlVales> ControlDeVales { get; set; }
        public DbSet<PROV_EstatusVale> PROV_EstatusVale { get; set; }
        public DbSet<PROV_TipoVales> TipoVales { get; set; }
        public DbSet<PROV_DetalleProducto> PROV_DetalleProducto { get; set; }
        public DbSet<PROV_DetalleVale> PROV_DetalleVale { get; set; }
        public DbSet<PROV_Producto> PROV_Producto { get; set; }
        public DbSet<PROV_Proveedor> PROV_Proveedor { get; set; }
        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<PuestoTrabajo> PuestoTrabajo { get; set; }
        public DbSet<PAT_EquipoComputo> EquipoComputo { get; set; }
        public DbSet<PAT_Monitor> PAT_Monitor { get; set; }
        public DbSet<PAT_Mouse> PAT_Mouse { get; set; }
        public DbSet<PAT_Teclado> PAT_Teclado { get; set; }
        public DbSet<PAT_Vehiculo> PAT_Vehiculo { get; set; }
        public DbSet<PAT_Mobiliario> PAT_Mobiliario { get; set; }
        public DbSet<PAT_EstatusEquipo> InventarioEstatus { get; set; }
    }
}
