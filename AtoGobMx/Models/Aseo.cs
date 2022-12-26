using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Aseo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AseoId { get; set; }
        public string NombreServicio { get; set; }
        public string EstablecimientoPublico { get; set; }
        public string Domicilio { get; set; }
        public string Objetivo { get; set; }
        public bool Archivado { get; set; }
        //public int? RutaId { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ArchivosAseo> ArchivosAseo { get; set; }
    }
}
