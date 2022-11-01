using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_EquipoComputo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipoComputoId { get; set; }
        public string? CodigoInventario { get; set; }
        public string Marca { get; set; }
        public string MemoriaRAM { get; set; }
        public string Almacenamiento { get; set; }
        public string Procesador { get; set; }
        public int AreaId { get; set; }
        public int DepartamentoId { get; set; }
        public int EstatusId { get; set; }
        public bool Archivado { get; set; }


        [ForeignKey("AreaId")]
        public Area? Area { get; set; }
        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }

        [ForeignKey("EstatusId")]
        public InventarioEstatus? InventarioEstatus { get; set; }
    }
}
