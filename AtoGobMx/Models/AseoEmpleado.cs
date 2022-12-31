using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class AseoEmpleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AseoEmpleadoId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? RutaId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }
        [ForeignKey("RutaId")]
        public Ruta? Ruta { get; set; }
    }
}
