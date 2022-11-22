using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class SERMED_Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }
        public int CategoriaMedicamentoId { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CantidadDisponible { get; set; }
        public bool Archivado { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<SERMED_ProductosReceta>? ProductosReceta { get; set; }
    }
}
