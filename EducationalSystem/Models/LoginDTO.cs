using EducationalSystem.Emuns;

namespace EducationalSystem.Models
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
