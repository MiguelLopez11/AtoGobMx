using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class SERMED_Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CitaId { get; set; }
        public string NombreCitante { get; set; }
        public DateTime FechaHora { get; set; }
        public int EmpleadoId { get; set; }
        public string Descripcion { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }

    }
}
