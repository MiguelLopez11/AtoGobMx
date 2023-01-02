using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ExpedienteVale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteValeId { get; set; }
        public int ControlValeId { get; set; }
        public bool Archivado  { get; set; }

        [ForeignKey("ControlValeId")]
        public PROV_ControlVales? ControlDeVales { get; set; }
    }
}
