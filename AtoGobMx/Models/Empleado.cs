
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
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ApellidoPaterno { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ApellidoMaterno { get; set; }
        [Required]
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        [Required]
        public bool Archivado { get; set; }
        public int? AreaId { get; set; }

        [ForeignKey("AreaId")]
        public Area? Area { get; set; }
        public int? UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario? usuario { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ExpedienteDigital>? ExpedientesDigitales { get; set; }
        
        //public virtual IEnumerable<Archivos>? Archivos { get; set; }
    }
}
