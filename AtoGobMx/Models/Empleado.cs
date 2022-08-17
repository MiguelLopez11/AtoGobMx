
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
        [Required]
        //public string? Estado { get; set; }
        //[Required]
        //public string? Municipio { get; set; }
        //public string? Localidad { get; set; }
        //[Required]
        //public string? Calle { get; set; }
        //[Required]
        //public int? NumeroExterior { get; set; }
        //public int? NumeroInterior { get; set; }
        //[Required]
        //public int? CodigoPostal { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        [Required]
        public bool Archivado { get; set; }
        public int? AreaId { get; set; }

        [ForeignKey("AreaId")]
        public Area? Area { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ExpedienteEmpleado>? ExpedienteEmpleados { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Usuario>? Usuarios { get; set; }
    }
}
