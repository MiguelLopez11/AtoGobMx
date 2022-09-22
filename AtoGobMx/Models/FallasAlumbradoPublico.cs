using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class FallasAlumbradoPublico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int FallaId { get; set; }
        public string TipoFalla { get; set; }
        public string DescripcionSolucion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public string Domicilio { get; set; }
        public string DescripcionDomicilio { get; set; }
        public bool Archivado   { get; set; }
       

    }
}
