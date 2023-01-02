using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class AseoVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AseoVehiculoId { get; set; }
        public int? RutaId { get; set; }
        public int? VehiculoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("VehiculoId")]
        public PAT_Vehiculo? Vehiculo { get; set; }
        [ForeignKey("RutaId")]
        public Ruta? Ruta { get; set; }
    }
}
