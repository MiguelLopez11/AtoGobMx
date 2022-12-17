using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Ruta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RutaId { get; set; }
        public string Nombre { get; set; }
        public string Horario { get; set; } 
        public string Observacion { get; set; }
        public bool Archivado { get; set; }
    }
}
