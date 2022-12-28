using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_AccesorioComputo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccesorioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public int EquipoComputoId { get; set; }
        public bool Archivado { get; set; }
        [ForeignKey("EquipoComputoId")]
        public PAT_EquipoComputo? EquipoComputo { get; set; }
    }
}
