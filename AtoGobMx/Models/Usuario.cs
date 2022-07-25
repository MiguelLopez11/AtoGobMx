using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DataType(DataType.Password)]
        public string? Contraseña { get; set; }
        [Required(ErrorMessage = "Confirmar contraseña es requerido")]
        [DataType(DataType.Password)]
        [Compare("Contraseña")]
        public string? ConfirmarContraseña{get;set;}
        [Required]
        public bool Archivado { get; set; }
        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

    }
}
