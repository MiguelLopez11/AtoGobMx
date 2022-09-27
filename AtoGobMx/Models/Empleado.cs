
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpleadoId { get; set; }
        [Required]
        [MaxLength(200)]
        public string? NombreCompleto { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        [Required]
        public bool Archivado { get; set; }
        public int? AreaId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? PuestoTrabajoId { get; set; }
        [ForeignKey("AreaId")]
        public Area? Area { get; set; }
        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }
        [ForeignKey("PuestoTrabajoId")]
        public PuestoTrabajo? PuestoTrabajo { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Usuario>? Usuarios { get; set; }
    }
}
