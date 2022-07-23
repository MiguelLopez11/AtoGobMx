using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int EmpleadoId { get; set; }
        [ForeignKey("Area")]
        public int? AreaId { get; set; }
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
        public string? Direccion { get; set; }
        [Required]
        public string? CURP { get; set; }
        [Required]
        public string? RFC { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        [Required]
        public DateTime? FechaBaja { get; set; }
        [Required]
        public bool Archivado { get; set; }
        [JsonIgnore]
        public  Area? Area { get; set; }
    }
}
