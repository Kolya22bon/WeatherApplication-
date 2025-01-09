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

            // �������� �� ������� ������������� ������������ � ����� ������ ��� email
            var existingUserByUsername = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == Input.Username);
            var existingUserByEmail = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == Input.Email);

            if (existingUserByUsername != null)
            {
                ModelState.AddModelError(string.Empty, "������������ � ����� ������ ��� ����������.");
                return Page();
            }

            if (existingUserByEmail != null)
            {
                ModelState.AddModelError(string.Empty, "������������ � ����� email ��� ����������.");
                return Page();
            }

            // ������ ������ ������������
            var user = new ApplicationUser
            {
                Username = Input.Username,
                Email = Input.Email,
                PasswordHash = Input.Password
            };

            // ��������� ������������ � ���� ������
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // �������������� �� ������� ��������
            //return RedirectToPage("/Account/Login");  // ��������������� �� ������� ��������
            return Redirect("/Index"); // �������������� �� https://localhost:7017/Index
        }

    }
}
