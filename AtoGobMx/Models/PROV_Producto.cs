using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PROV_Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<PROV_DetalleVale>? PROV_DetalleVale { get; set; }

    }
}
