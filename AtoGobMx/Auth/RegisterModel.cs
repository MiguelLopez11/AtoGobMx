using System.ComponentModel.DataAnnotations;

namespace AtoGobMx.Auth
{
    public class RegisterModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public int? EmpleadoId { get; set; }
    }
}
