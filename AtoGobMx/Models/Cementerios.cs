using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Cementerios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CementeriosId { get; set; }
        public string NombrePropietario { get; set; }
        public int NumeroEspasios { get; set; }
        public int MetrosCorrespondientes { get; set; }
        public int EspaciosDisponibles { get; set; }
        public bool Archivado { get; set; }

    }
}
