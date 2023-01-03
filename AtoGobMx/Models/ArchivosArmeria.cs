using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AtoGobMx.Models
{
    public class ArchivosArmeria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivoArmeriaId { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(100)]
        public string? TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int ArmaId { get; set; }

        [ForeignKey("ArmaId")]
        public PAT_Armeria? Armeria { get; set; }
    }
}
