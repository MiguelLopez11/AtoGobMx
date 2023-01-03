using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AtoGobMx.Models
{
    public class PAT_ArmeriaCartucho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartuchoId { get; set; }
        public string Nomenclatura { get; set; }
        public string Marca { get; set; }
        public string TipoArma { get; set; }
        public int Calibre { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public double Costo { get; set; }
        public int ArmeriaId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("ArmeriaId")]
        public PAT_Armeria? Armeria { get; set; }
    }
}
