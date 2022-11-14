using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PROV_EstatusVale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstatusValeId { get; set; }
        public string EstatusVale { get; set; }
        public string Descripcion { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<PROV_ControlVales>? ControlDeVales { get; set; }
    }
}
