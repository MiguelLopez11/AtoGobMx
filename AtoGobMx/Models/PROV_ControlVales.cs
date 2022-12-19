using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PROV_ControlVales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ControlValeId { get; set; }
        public DateTime? FechaEmicion { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public int? DepartamentoId { get; set; }
        //public int? AreaId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? ProveedorId { get; set; }
        public int? EstatusValeId { get; set; }
        public int? TipoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }
        //[ForeignKey("AreaId")]
        //public Area? Area { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }
        [ForeignKey("ProveedorId")]
        public PROV_Proveedor? PROV_Proveedor { get; set; }
        [ForeignKey("EstatusValeId")]
        public PROV_EstatusVale? PROV_EstatusVale { get; set; }
        [ForeignKey("TipoId")]
        public PROV_TipoVales? TipoVales { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<PROV_DetalleVale>? PROV_DetalleVales { get; set; }
    }
}