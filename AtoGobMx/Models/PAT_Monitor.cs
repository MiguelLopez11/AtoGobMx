using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_Monitor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MonitorId { get; set; }
        public string? Marca { get; set; }
        public int? Pulgadas { get; set; }
        public int? EquipoComputoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EquipoComputoId")]
        public PAT_EquipoComputo? EquipoComputo { get; set; }

    }
}
