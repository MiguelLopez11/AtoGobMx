using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class SERMED_Receta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecetaId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? EstatusRecetaId { get; set; }
        public string? diagnostico { get; set; }
        public DateTime? FechaAlta { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }
        [ForeignKey("EstatusRecetaId")]
        public SERMED_EstatusReceta? EstatusReceta { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<SERMED_ProductosReceta>? ProductosReceta { get; set; }
    }
}
