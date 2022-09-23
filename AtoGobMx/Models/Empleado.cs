
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
        [Required]
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        [Required]
        public bool Archivado { get; set; }
        public int? AreaId { get; set; }
        //public int? UsuarioId { get; set; }

        public int? expedienteDigitalId { get; set; }

        [ForeignKey("expedienteDigitalId")]
        public ExpedienteDigital? ExpedienteDigital { get; set; }

        [ForeignKey("AreaId")]
        public Area? Area { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Usuario>? Usuarios { get; set; }
        //[ForeignKey("UsuarioId")]
        //public Usuario? usuario { get; set; }

        //[JsonIgnore]
        //public virtual IEnumerable<ExpedienteDigital>? ExpedientesDigitales { get; set; }

        //public virtual IEnumerable<Archivos>? Archivos { get; set; }
    }
}
