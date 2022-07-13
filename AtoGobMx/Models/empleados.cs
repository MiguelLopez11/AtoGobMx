using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string direccion { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        
        public bool archivado { get; set; }
    }
}
