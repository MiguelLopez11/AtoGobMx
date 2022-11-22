using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class OP_Empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpleadoObrasId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? ObraId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }
        [ForeignKey("ObraId")]
        public OP_Obras? ObrasPublicas { get; set; }
    }
}
