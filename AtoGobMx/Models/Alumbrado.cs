using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Alumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AlumbradoId { get; set; }
        public string Tarea { get; set; }
        public string DescripcionSolucion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public string Domicilio { get; set; }
        public string DescripcionDomicilio { get; set; }
        public bool Archivado { get; set; }
        public int? expedienteAlumbradoId { get; set; }


        [ForeignKey("expedienteAlumbradoId")]
        public ExpedienteAlumbrado? ExpedienteAlumbrado { get; set; }
    }
}
