using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChroniclerWeb.Pages.Campaigns
{
    [AutoValidateAntiforgeryToken]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Campaign Campaign { get; set; } = new();

        [BindProperty]
        public string? CoverImageUrl { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User)!;
            Campaign.OwnerId = userId;
            Campaign.CreatedAt = DateTime.UtcNow;
            Campaign.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(CoverImageUrl))
            {
                Campaign.LogoUrl = CoverImageUrl;
            }

            ModelState.Remove("Campaign.Owner");
            ModelState.Remove("Campaign.OwnerId");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["Error"] = "Please fix: " + string.Join("; ", errors);
                return Page();
            }

            _context.Campaigns.Add(Campaign);
            await _context.SaveChangesAsync(); // Save Campaign first to get its Id

            // Add owner as GM member
            var membership = new CampaignMember
            {
                CampaignId = Campaign.Id,
                UserId = userId,
                Role = CampaignRole.GameMaster,
                CanAddNotes = true
            };
            _context.CampaignMembers.Add(membership);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Campaign '{Campaign.Name}' created successfully!";
            return RedirectToPage("/Campaigns/Details", new { id = Campaign.Id });
        }
    }
}