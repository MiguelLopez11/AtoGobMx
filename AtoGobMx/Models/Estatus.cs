using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Estatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstatusId { get; set; }
        public string NombreEstatus { get; set; }
        public string Descripcion { get; set; }
        public bool Archivado  { get; set; }

    }
}
