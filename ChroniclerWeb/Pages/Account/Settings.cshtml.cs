using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using ChroniclerWeb.Services.Avatar;


namespace ChroniclerWeb.Pages.Account
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AvatarService _avatarService;

        public SettingsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AvatarService avatarService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _avatarService = avatarService;
        }

        public ApplicationUser CurrentUser { get; set; } = null!;
        public IEnumerable<string> DefaultAvatars => _avatarService.GetDefaultAvatars();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            CurrentUser = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? AvatarFile, string SelectedAvatar)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (!string.IsNullOrEmpty(Request.Form["DisplayName"]))
            {
                user.DisplayName = Request.Form["DisplayName"];
            }

            // Handle avatar upload
            if (AvatarFile != null && AvatarFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await AvatarFile.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                user.AvatarData = Convert.ToBase64String(fileBytes);
                user.AvatarContentType = AvatarFile.ContentType;
            }
            else if (!string.IsNullOrEmpty(SelectedAvatar))
            {
                // Handle selection of default avatar
                user.AvatarData = SelectedAvatar;
                user.AvatarContentType = "image/svg+xml";
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdatePreferencesAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (!string.IsNullOrEmpty(Request.Form["Language"]))
            {
                user.PreferredLanguage = Request.Form["Language"];
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            TempData["Success"] = "Preferences updated successfully!";
            return RedirectToPage();
        }

        public string GetAccountAge()
        {
            if (CurrentUser?.CreatedAt == null)
                return "Unknown";

            var createdAt = CurrentUser.CreatedAt;
            var now = DateTime.UtcNow;
            var span = now - createdAt;

            if (span.TotalDays < 30)
            {
                return $"{(int)span.TotalDays} day(s)";
            }
            else if (span.TotalDays < 365)
            {
                var months = (int)(span.TotalDays / 30);
                return $"{months} month(s)";
            }
            else
            {
                var years = (int)(span.TotalDays / 365);
                var months = (int)((span.TotalDays % 365) / 30);
                if (months > 0)
                    return $"{years} year(s), {months} month(s)";
                return $"{years} year(s)";
            }
        }
    }
}