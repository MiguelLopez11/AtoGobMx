using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PuestoTrabajo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PuestoTrabajoId { get; set; }
        public string? Nombre { get; set; }

        public int? DepartamentoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }
    }
}
