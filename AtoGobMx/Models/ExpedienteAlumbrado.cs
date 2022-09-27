using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class ExpedienteAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteAlumbradoId { get; set; } 
        public string Tarea { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public string Domicilio { get; set; }
        public string DescripcionDomicilio { get; set; }
        public string DescripcionSolucion { get; set; }

        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Alumbrado>? Alumbrado { get; set; }

        //[JsonIgnore]
        //public virtual IEnumerable<Archivos>? Archivos { get; set; }

    }
}
