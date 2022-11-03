using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class PAT_TipoMobiliario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoMobiliarioId { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public bool Archivado { get; set; }
    }
}
