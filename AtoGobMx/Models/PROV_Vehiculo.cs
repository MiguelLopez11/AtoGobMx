using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PROV_Vehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvVehiculoId { get; set; }
        public int? VehiculoId { get; set; }
        public int? ControlValeId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("VehiculoId")]
        public PAT_Vehiculo? Vehiculo { get; set; }
        [ForeignKey("ControlValeId")]
        public PROV_ControlVales? ControlDeVales { get; set; }
    }
}
