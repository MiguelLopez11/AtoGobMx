using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class SERMED_ExpedienteMedico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteMedicoId { get; set; }
        [Required]
        public int EmpleadoId { get; set; }
        [Required]
        public float Estatura { get; set; }
        [Required]
        public float Peso { get; set; }
        [Required]
        public string TipoSangre { get; set; }
        [Required]
        public string Sexo { get; set; }
        public string? Alergias { get; set; }
        public string? Discapacidad { get; set; }
        public string? AntecedentesPersonales { get; set; }
        public string? AntecedentesFamiliares { get; set; }
        public bool Archivado { get; set; }


        [ForeignKey("EmpleadoId")]
        public Empleado? Empleados { get; set; }
    }
}
