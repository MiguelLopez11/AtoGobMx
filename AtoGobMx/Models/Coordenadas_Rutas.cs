using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class Coordenadas_Rutas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  CoordenadaId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int RutaId { get; set; }
        public bool Archivado { get; set; }

        public int OrdenCoordenada { get; set; }
        [ForeignKey("RutaId")]
        public Ruta? Ruta { get; set; }
    }
}
