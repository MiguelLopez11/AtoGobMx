using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PAT_EquipoComputo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EquipoComputoId { get; set; }
        public string? CodigoInventario { get; set; }
        public string? Caracteristicas { get; set; } 
        public DateTime? fechaAdquisicion { get; set; }
        public double? Costo { get; set; }
        public string? NumeroSerie { get; set; }
        public int DepartamentoId { get; set; }
        public int EstatusEquipoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }

        [ForeignKey("EstatusEquipoId")]
        public PAT_EstatusEquipo? EstatusEquipo { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<PAT_AccesorioComputo>? AccesorioComputo { get; set; }
    }
}
