using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class VehiculosAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehiculoAlumbradoId { get; set; }
        public int? EstatusVehiculoId { get; set; }
        public int? VehiculoId { get; set; }
        public int? ExpedienteAlumbradoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EstatusVehiculoId")]
        public PAT_EstatusVehiculo? EstatusVehiculo { get; set; }
        [ForeignKey("VehiculoId")]
        public PAT_Vehiculo? Vehiculo { get; set; }
        [ForeignKey("ExpedienteAlumbradoId")]
        public ExpedienteAlumbrado? ExpedienteAlumbrado { get; set; }

    }
}
