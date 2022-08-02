using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class FallasAlumbradoPublico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int FallaId { get; set; }
        public string NombreFalla { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string DescripcionDomicilio { get; set; }
        public bool Archivado   { get; set; }


    }
}
