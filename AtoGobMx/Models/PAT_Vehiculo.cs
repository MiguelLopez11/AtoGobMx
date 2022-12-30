using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PAT_Vehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehiculoId { get; set; }
        public string Nomenclatura { get; set; }
        public int? ProveedorId { get; set; }
        public string? NumeroFactura { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Color { get; set; } 
        public string? Placa { get; set; }
        public string? NumeroSerie { get; set; }
        public int? AñoLanzamiento { get; set; }
        public int? Puertas { get; set; }
        public string? Transmisión { get; set; }
        public string? Version { get; set; }
        public int? Cilindros { get; set; }
        public string Observaciones { get; set; }
        public int? EstatusVehiculoId { get; set; }

        public bool Archivado { get; set; }

        [ForeignKey("EstatusVehiculoId")]
        public PAT_EstatusVehiculo? EstatusVehiculo { get; set; }
        [ForeignKey("ProveedorId")]
        public PROV_Proveedor? Proveedor { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ExpedienteAlumbrado>? ExpedienteAlumbrado  { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<VehiculosAlumbrado>? VehiculosAlumbrado { get; set; }

    }
}
