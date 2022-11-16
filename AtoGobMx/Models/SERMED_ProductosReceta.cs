using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class SERMED_ProductosReceta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoRecetaId { get; set; }
        public int RecetaId { get; set; }
        public int ProductoId { get; set; }
        public int? cantidad { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }
        [ForeignKey("RecetaId")]
        public SERMED_Receta? Receta { get; set; }
        [ForeignKey("ProductoId")]
        public SERMED_Producto? Producto { get; set; }

    }
}
