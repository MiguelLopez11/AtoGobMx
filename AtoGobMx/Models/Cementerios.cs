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
        public float MetrosCorrespondientes { get; set; }
        public float Longitud { get; set; }
        public float Latitud { get; set; }
        public int EspaciosDisponibles { get; set; }
        public int? DireccionId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("DireccionId")]
        public DireccionCementerio? DireccionCementerio { get; set; }

    }
}
