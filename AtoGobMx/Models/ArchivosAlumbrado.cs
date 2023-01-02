using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ArchivosAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivoAlumbradoId { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(100)]
        public string? TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int AlumbradoId { get; set; }

        [ForeignKey("AlumbradoId")]
        public Alumbrado? Alumbrado { get; set; }
    }
}
