using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idEmpleado { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string ApellidoPaterno { get; set; }
        [Required]
        [MaxLength(100)]
        public string ApellidoMaterno { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string CURP { get; set; }
        [Required]
        public string RFC { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public bool Archivado { get; set; }

        [Required]
        public int AreaTrabajoId { get; set; }

        
        public virtual Areas? AreaTrabajo { get; set; }
    }
}
