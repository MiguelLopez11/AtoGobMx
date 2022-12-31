using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class OP_Vehiculos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OpVehiculoId { get; set; }
        public int? VehiculoId { get; set; }
        public int? ObraId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("VehiculoId")]
        public PAT_Vehiculo? Vehiculo { get; set; }
        [ForeignKey("ObraId")]
        public OP_Obras? ObrasPublicas { get; set; }
    }
}
