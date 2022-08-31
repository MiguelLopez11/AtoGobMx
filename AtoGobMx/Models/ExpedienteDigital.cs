using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class ExpedienteDigital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteDigitalId { get; set; }
        public int EmpleadoId { get; set; }
        public string? Estado { get; set; }
        public string? Municipio { get; set; }
        public string? Localidad { get; set; }
        public string? Calle { get; set; }
        public int? NumeroExterior { get; set; }
        public int? NumeroInterior { get; set; }
        public int? CodigoPostal { get; set; }
        public string? CorreoElectronico { get; set; }
        [Required]
        public bool Archivado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }

        //[JsonIgnore]
        //public virtual IEnumerable<Empleado>? Empleados { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Archivos>? Archivos { get; set; }

    }
}
