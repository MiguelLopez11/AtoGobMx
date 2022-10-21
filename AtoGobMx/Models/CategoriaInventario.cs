using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class CategoriaInventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaInventarioId { get; set; }
        public string NombreCategoria { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }
    }
}
