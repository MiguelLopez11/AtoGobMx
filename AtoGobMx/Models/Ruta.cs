using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Ruta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RutaId { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        //public int kilometros { get; set; }
        public string Obsevacion { get; set; }
        public bool Archivado { get; set; }
    }
}
