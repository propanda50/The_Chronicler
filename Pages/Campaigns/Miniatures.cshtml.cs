using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Campaigns
{
    [Authorize]
    public class MiniaturesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public MiniaturesModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = "";
        public List<MiniatureDto> Miniatures { get; set; } = new();

        public class MiniatureDto
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public string Color { get; set; } = "#d4a843";
            public float PositionX { get; set; }
            public float PositionZ { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            // Load miniatures from campaign data (using a simple JSON storage)
            // For now, return empty - miniatures are stored client-side
            Miniatures = new List<MiniatureDto>();

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync([FromBody] SaveMiniatureRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            // Save miniature position data
            // Could save to database or campaign settings
            return new JsonResult(new { success = true });
        }

        public class SaveMiniatureRequest
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public string Color { get; set; } = "";
            public float PositionX { get; set; }
            public float PositionZ { get; set; }
        }
    }
}
