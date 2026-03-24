using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;

namespace ChroniclerWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required, MaxLength(100)]
            [Display(Name = "Display Name")]
            public string DisplayName { get; set; } = string.Empty;

            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6)]
            public string Password { get; set; } = string.Empty;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            [MaxLength(10)]
            public string PreferredLanguage { get; set; } = "en";

            public PlayStyle? PlayStyle { get; set; }

            public ExperienceLevel? ExperienceLevel { get; set; }

            [MaxLength(1000)]
            public string? Bio { get; set; }
        }

        public void OnGet() { }

        public IActionResult OnPostExternalLogin(string provider, string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/Dashboard");
            var redirectUrl = Url.Page("/Account/ExternalLoginCallback", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                DisplayName = Input.DisplayName,
                PreferredLanguage = Input.PreferredLanguage,
                PlayStyle = Input.PlayStyle,
                ExperienceLevel = Input.ExperienceLevel,
                Bio = Input.Bio,
                ProfileCompleted = true
            };

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0 && file.Length < 5 * 1024 * 1024)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    user.AvatarData = Convert.ToBase64String(memoryStream.ToArray());
                    user.AvatarUrl = $"data:{file.ContentType};base64,{user.AvatarData}";
                }
            }

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Player");
                await _signInManager.SignInAsync(user, isPersistent: false);

                var langName = Input.PreferredLanguage switch { "nl" => "Dutch", "fr" => "French", _ => "English" };
                TempData["Success"] = $"Welcome, {user.DisplayName}! Language set to {langName}. Change it anytime in Settings.";

                return RedirectToPage("/Dashboard/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
