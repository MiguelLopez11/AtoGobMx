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
        public string Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<empleados>? Empleados { get; set; }
        
    }
}
