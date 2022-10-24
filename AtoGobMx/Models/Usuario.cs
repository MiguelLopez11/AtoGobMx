using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        [Required]
        public string? NombreUsuario { get; set; }
        [Required(ErrorMessage = "La contraseña es requerido")]
        public string? Contraseña { get; set; }
        [MinLength(8)]
        [Compare("Contraseña")]
        public string? ConfirmarContraseña { get; set; }
        public int? RoleId { get; set; }
        public int? EmpleadoId { get; set; }
        [Required]
        public bool Archivado { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleado? Empleado { get; set; }
    }
}
