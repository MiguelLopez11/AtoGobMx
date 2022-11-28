using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace AtoGobMx.Models
{
    public class PAT_EstatusVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstatusVehiculoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<VehiculosAlumbrado>? VehiculosAlumbrado { get; set; }
    }
}
