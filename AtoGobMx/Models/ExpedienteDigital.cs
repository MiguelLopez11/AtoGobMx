using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ExpedienteDigital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteDigitalId { get; set; }
        public int EmpleadoId { get; set; }
        [Required]
        public string? Estado { get; set; }
        [Required]
        public string? Municipio { get; set; }
        public string? Localidad { get; set; }
        [Required]
        public string? Calle { get; set; }
        [Required]
        public int? NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        [Required]
        public int? CodigoPostal { get; set; }
        public string? CorreoElectronico { get; set; }
        //public DateTime FechaIngreso { get; set; }
        //public DateTime FechaBaja { get; set; }
        public bool Archivado { get; set; }
        
    }
}
