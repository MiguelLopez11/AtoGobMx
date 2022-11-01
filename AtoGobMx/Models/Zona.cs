using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Zona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int zonaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Archivado { get; set; }
        //public int? RutaId { get; set; }
    }
}
