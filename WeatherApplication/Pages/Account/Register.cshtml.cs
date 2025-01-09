using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Data;
using WeatherApplication.Models;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;

namespace WeatherApplication.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisterModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Проверка на наличие существующего пользователя с таким именем или email
            var existingUserByUsername = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == Input.Username);
            var existingUserByEmail = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == Input.Email);

            if (existingUserByUsername != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким именем уже существует.");
                return Page();
            }

            if (existingUserByEmail != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким email уже существует.");
                return Page();
            }

            // Создаём нового пользователя
            var user = new ApplicationUser
            {
                Username = Input.Username,
                Email = Input.Email,
                PasswordHash = Input.Password
            };

            // Сохраняем пользователя в базе данных
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Перенаправляем на главную страницу
            //return RedirectToPage("/Account/Login");  // Перенаправление на главную страницу
            return Redirect("/Index"); // Перенаправляем на https://localhost:7017/Index
        }

    }
}
