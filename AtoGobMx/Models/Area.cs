using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //[Newtonsoft.Json.JsonIgnore]
        public ICollection<Empleado> Empleados { get; set; }
    }
}
