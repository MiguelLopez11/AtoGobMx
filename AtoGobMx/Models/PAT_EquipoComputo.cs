using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_EquipoComputo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipoComputoId { get; set; }
        public string Marca { get; set; }
        public int MemoriaRAM { get; set; }
        public string Almacenamiento { get; set; }
        public string Procesador { get; set; }

        public int EstatusId { get; set; }
        public bool Archivado { get; set; }



        [ForeignKey("EstatusId")]
        public InventarioEstatus? InventarioEstatus { get; set; }
    }
}
