using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class OP_EstatusObras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstatusObraId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<OP_Obras>? ObrasPublicas { get; set; }
    }
}
