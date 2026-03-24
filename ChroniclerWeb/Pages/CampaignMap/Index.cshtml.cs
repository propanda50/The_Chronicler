using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace ChroniclerWeb.Pages.CampaignMap
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public List<LocationDto> Locations { get; set; } = new();

        public class LocationDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Region { get; set; } = "";
            public string Type { get; set; } = "";
            public double? MapX { get; set; }
            public double? MapY { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            Locations = await _context.Locations
                .Where(l => l.CampaignId == campaignId)
                .Select(l => new LocationDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Region = l.Region,
                    Type = l.Type,
                    MapX = l.MapX,
                    MapY = l.MapY
                })
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddLocationAsync([FromBody] AddLocationRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(request.CampaignId, userId))
                return Forbid();

            var location = new Location
            {
                Name = request.Name,
                Description = request.Description,
                CampaignId = request.CampaignId,
                MapX = request.MapX,
                MapY = request.MapY,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return new JsonResult(new { id = location.Id });
        }

        public class AddLocationRequest
        {
            public int CampaignId { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public double MapX { get; set; }
            public double MapY { get; set; }
        }
    }
}
