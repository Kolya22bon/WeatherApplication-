using Microsoft.AspNetCore.Identity;

namespace WeatherApplication.Models
{
    // Наследуем от IdentityUser, чтобы использовать стандартные свойства Identity
    public class ApplicationUser : IdentityUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Хранить хэш пароля
    }
}
