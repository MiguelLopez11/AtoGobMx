using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ExpedienteEmpleado
    {
        [Key]
        public int ExpedienteEmpleadoId { get; set; }

        public int EmpleadoId { get; set; }
        [Required]
        public string? CURP { get; set; }
        [Required]
        public string? RFC { get; set; }
        //public string? NombreFotografia { get; set; }
        //v[DisplayName("Image Name")]
        //public IFormFile? Fotografia { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }
    }
}
