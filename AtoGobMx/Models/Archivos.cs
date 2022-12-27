using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Archivos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivoId { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(100)]
        public string? TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int ExpedienteDigitalId { get; set; }
        //public int ExpedienteAlumbradoId { get; set; }

        [ForeignKey("ExpedienteDigitalId")]
        public ExpedienteDigital? expedienteDigital { get; set; }
        //[ForeignKey("")]

        //[JsonIgnore]
        //public virtual IEnumerable<ExpedienteDigital>? ExpedienteDigital { get; set; }
    }
}
