using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ArchivosObras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivosObrasId { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(100)]
        public string? TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int ObraId { get; set; }

        [ForeignKey("ObraId")]
        public OP_Obras? OP_Obras { get; set; }
    }
}
