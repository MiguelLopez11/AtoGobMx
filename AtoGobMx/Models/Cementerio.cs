
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Cementerio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CementerioId { get; set; }
        public string NombreCementerio { get; set; }
        public string Comunidad { get; set; }
        public string Localidad { get; set; }
        public string Calle { get; set; }
        public int NumeroExterior { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Gabetas>? Gabetas { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ArchivosCementerios>? ArchivosCementerios { get; set; }
    }
}
