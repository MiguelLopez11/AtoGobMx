using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Areas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreasId { get; set; }
        public string Nombre { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
