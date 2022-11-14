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
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public int subtotal { get; set; }
        public int? ProductoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("ProductoId")]
        public PROV_Producto? PROV_Producto { get; set; }

        //[JsonIgnore]
        //public virtual IEnumerable<PROV_ControlVales>? ControlDeVales { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<PROV_DetalleProducto>? PROV_DetalleProducto { get; set; }
    }
}