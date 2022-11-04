
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace AtoGobMx.Models
{
    public class PAT_CategoriaArmeria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaArmeriaId { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<PAT_Armeria>? Armeria { get; set; }
    }
}
