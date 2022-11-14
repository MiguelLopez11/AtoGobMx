using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PROV_TipoVales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoId { get; set; }
        public string NombreVale { get; set; }
        public string Descripcion { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<PROV_ControlVales>? ControlDeVales { get; set; }

    }
}
