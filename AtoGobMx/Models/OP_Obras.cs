using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class OP_Obras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ObraId { get; set; }
        public string Nombre { get; set; }
        public string? Encargado { get; set; }
        public string? OperadorObra { get; set; }
        public string? OperadorVehiculo { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Descripcion { get; set; }
        public int? EstatusObraId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("EstatusObraId")]
        public OP_EstatusObras? OP_EstatusObras { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<OP_Empleados>? OP_Empleados { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ArchivosObras>? ArchivosObras { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<OP_Vehiculos>? OP_Vehiculos { get; set; }

    }
}
