using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class ExpedienteFallas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteFallaId { get; set; } 
        public string TipoFalla { get; set; }
        public string TipoOrden { get; set; }
        public string Domicilio { get; set; }
        public string MarcaIluminaria { get; set; } 
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<FallasAlumbradoPublico>? FallasAlumbradoPublico { get; set; }

        //[JsonIgnore]
        //public virtual IEnumerable<Archivos>? Archivos { get; set; }

    }
}
