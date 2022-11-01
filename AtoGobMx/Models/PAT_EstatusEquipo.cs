using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PAT_EstatusEquipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstatusEquipoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<PAT_EquipoComputo>? EquipoComputo { get; set; }
    }
}
