using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class DireccionCementerio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DireccionId { get; set; }
        public string NombreCementerio { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string Calle { get; set; }
        public int NumeroExterior { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Cementerios>? Cementerios { get; set; }
    }
}
