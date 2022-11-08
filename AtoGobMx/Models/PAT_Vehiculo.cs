using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_Vehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehiculoId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; } 
        public string Placa { get; set; }
        public string NumeroSerie { get; set; }
        public int AñoLanzamiento { get; set; }
        public int Puertas { get; set; }
        public string Transmisión { get; set; }
        public int EstatusVehiculoId { get; set; }
        [ForeignKey("EstatusVehiculoId")]
        public PAT_EstatusVehiculo? EstatusVehiculo { get; set; }

        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ExpedienteAlumbrado>? ExpedienteAlumbrado  { get; set; }

    }
}
