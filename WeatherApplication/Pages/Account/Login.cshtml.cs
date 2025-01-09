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

        // ����� ��������� GET-�������
        public void OnGet()
        {
        }

        // ����� ��������� POST-������� ��� �����
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ���������, �������� �� �������� Username ��� Email
            var user = await _signInManager.UserManager.FindByNameAsync(Input.Username);
            if (user == null)
            {
                // ���� �� ������� �� ����� ������������, ������� ������ �� email
                user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "������������ � ����� ������ ��� email �� ������.");
                return Page();
            }

            // ������� �������������� ����� SignInManager
            var signInResult = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);

            if (signInResult.Succeeded)
            {
                return Redirect("/Index"); // �������������� �� https://localhost:7017/Index
            }

            // ���� ���� �� ������
            ModelState.AddModelError(string.Empty, "�������� ��� ������������, ����������� ����� ��� ������.");
            return Page();
        }


    }
}
