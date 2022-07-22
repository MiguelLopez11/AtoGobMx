using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Areas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        [Required]
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<empleados> Empleados { get; set; }
    }
}
