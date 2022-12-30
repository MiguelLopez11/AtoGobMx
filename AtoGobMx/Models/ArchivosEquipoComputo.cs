using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class ArchivosEquipoComputo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivoEquipoId { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(100)]
        public string? TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int EquipoComputoId { get; set; }

        [ForeignKey("EquipoComputoId")]
        public PAT_EquipoComputo? EquipoComputo { get; set; }
    }
}
