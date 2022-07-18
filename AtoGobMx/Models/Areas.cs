using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Areas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        public string Nombre { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }

        [JsonIgnore]
        public ICollection<empleados>? Empleados { get; set; }
    }
}
