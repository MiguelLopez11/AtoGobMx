using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class PROV_Proveedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string RepresentanteLegal { get; set; }
        public string Direccion { get; set; }
        [MaxLength(10)]
        public int Telefono { get; set; }
        public string Email { get; set; }
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<PROV_ControlVales>? ControlDeVales { get; set; }
    }
}
