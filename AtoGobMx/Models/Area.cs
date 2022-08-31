using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        [Required]
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public bool Archivado { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<Empleado>? Empleados { get; set; }
    }
}
