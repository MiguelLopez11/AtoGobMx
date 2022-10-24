using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventarioId { get; set; }
        public string Folio { get; set; }
        public Boolean? isOnDependency { get; set; }
        public string? Dependencia { get; set; }
        public int? DepartamentoId { get; set; }
        public int? CategoriaInventarioId { get; set; }
        public int? InventarioEstatusId { get; set; }
        public string Descripción { get; set; }
        public bool Archivado { get; set; }
        //RELACIONES
        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }
        [ForeignKey("InventarioEstatusId")]
        public InventarioEstatus? InventarioEstatus { get; set; }

        [ForeignKey("CategoriaInventarioId")]
        public CategoriaInventario? CategoriaInventario { get; set; }


    }
}
