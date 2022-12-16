using Microsoft.AspNetCore.Identity;

namespace AtoGobMx.Models
{
    public class Usuario : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
