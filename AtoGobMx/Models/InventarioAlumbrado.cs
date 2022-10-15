using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class InventarioAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventarioAlumbradoId { get; set; }
        //public string Tarea { get; set; }
        //public DateTime FechaAlta { get; set; }
        //public DateTime FechaBaja { get; set; }
        //public string Domicilio { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Alumbrado>? Alumbrado { get; set; }

    }
}
