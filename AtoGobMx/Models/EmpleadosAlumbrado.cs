using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class EmpleadosAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlumbradoEmpleadoId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? ExpedienteAlumbradoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }

        [ForeignKey("ExpedienteAlumbradoId")]
        public ExpedienteAlumbrado? ExpedienteAlumbrado { get; set; }

    }
}
