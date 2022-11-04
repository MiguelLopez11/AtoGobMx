using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_Armeria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArmaId { get; set; }
        public string Nomenclatura { get; set; }
        public int CategoriaArmeriaId { get; set; }
        public string Calibre { get; set; }
        public int EmpleadoId { get; set; }

        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }

    }
}
