using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ArchivosCementerios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivosCementerioId { get; set; }
        public string Nombre { get; set; }
        public string TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int CementerioId { get; set; }

        [ForeignKey("CementerioId")]
        public Cementerio? Cementerio { get; set; }

    }
}
