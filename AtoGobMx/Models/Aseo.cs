
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Aseo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AseoId { get; set; }
        public string Nombre { get; set; }
        public string Problema { get; set; }
        public DateTime? Fecha { get; set; }
        public string Domicilio { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }
        //public int? RutaId { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ArchivosAseo>? ArchivosAseo { get; set; }
    }
}
