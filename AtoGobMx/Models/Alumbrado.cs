using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Alumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AlumbradoId { get; set; }
        public string? NombreObra { get; set; }
        public string DescripcionProblema { get; set; }
        public string Domicilio { get; set; }
        public string DescripcionDomicilio { get; set; }
        public bool Archivado { get; set; }
        public bool TieneExpediente { get; set; }
        public int? EstatusId { get; set; }
        public int? TareaTipoId { get; set; }

        [ForeignKey("EstatusId")]
        public EstatusAlumbrado? Estatus { get; set; }

        [ForeignKey("TareaTipoId")]
        public TareaTipoAlumbrado? TareaTipoAlumbrado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ExpedienteAlumbrado>? ExpedienteAlumbrado { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<ArchivosAlumbrado>? ArchivosAlumbrado { get; set; }

        //public string Tarea { get; set; }
        //public string DescripcionSolucion { get; set; }
        //public DateTime FechaAlta { get; set; }
        //public DateTime FechaBaja { get; set; }

        //public int? ExpedienteAlumbradoId { get; set; }
        //public int? TareaTipoId { get; set; }
        //[ForeignKey("ExpedienteAlumbradoId")]
        //public ExpedienteAlumbrado? ExpedienteAlumbrado { get; set; }
    }
}
