using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using WeatherApplication.Data;
using WeatherApplication.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WeatherApplication.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
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

        // Метод обработки GET-запроса
        public void OnGet()
        {
        }

        // Метод обработки POST-запроса для входа
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Проверяем, является ли введённый Username или Email
            var user = await _signInManager.UserManager.FindByNameAsync(Input.Username);
            if (user == null)
            {
                // Если не найдено по имени пользователя, пробуем искать по email
                user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким именем или email не найден.");
                return Page();
            }

            // Попытка аутентификации через SignInManager
            var signInResult = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);

            if (signInResult.Succeeded)
            {
                return Redirect("/Index"); // Перенаправляем на https://localhost:7017/Index
            }

            // Если вход не удался
            ModelState.AddModelError(string.Empty, "Неверное имя пользователя, электронная почта или пароль.");
            return Page();
        }


    }
}
