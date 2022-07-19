using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idEmpleado { get; set; }
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string apellidoPaterno { get; set; }
        [Required]
        [MaxLength(100)]
        public string apellidoMaterno { get; set; }
        [Required]
        public DateTime fechaNacimiento { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string CURP { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public int AreaTrabajoId { get; set; }

        public bool archivado { get; set; }

       public virtual Areas AreaTrabajo { get; set; }

    }
}
