using AtoGobMx.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PROV_DetalleVale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DetalleValeId { get; set; }
        public int? Cantidad { get; set; }
        
        public float? IVA { get; set; }
        public float? Total { get; set; }
        public int? ControlValeId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("ControlValeId")]
        public PROV_ControlVales? PROV_ControlVale { get; set; }

        //[JsonIgnore]
        //public virtual IEnumerable<PROV_ControlVales>? ControlDeVales { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<PROV_DetalleProducto>? PROV_DetalleProducto { get; set; }
    }
}