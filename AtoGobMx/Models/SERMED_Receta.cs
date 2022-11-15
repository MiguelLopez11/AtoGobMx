using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class SERMED_Receta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecetaId { get; set; }
        [Required]
        public int EmpleadoId { get; set; }

    }
}
