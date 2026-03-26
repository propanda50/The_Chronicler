using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.CampaignMap
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
            _environment = environment;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string? MapUrl { get; set; }
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
            if (!await _campaignService.CanUserAccess(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            
            if (!string.IsNullOrEmpty(campaign?.MapData))
            {
                if (campaign.MapData.StartsWith("data:"))
                {
                    MapUrl = campaign.MapData;
                }
                else
                {
                    var contentType = campaign.MapContentType ?? "image/jpeg";
                    MapUrl = $"data:{contentType};base64,{campaign.MapData}";
                }
            }
            else if (!string.IsNullOrEmpty(campaign?.MapUrl))
            {
                MapUrl = campaign.MapUrl;
            }

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
                Description = request.Description ?? "",
                Region = request.Region ?? "",
                Type = request.Type ?? "",
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

        public async Task<IActionResult> OnPostGenerateMapAsync([FromBody] GenerateMapRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(request.CampaignId, userId))
                return Forbid();

            var campaign = await _context.Campaigns.FindAsync(request.CampaignId);
            if (campaign == null)
                return NotFound();

            var styleNames = new Dictionary<string, string>
            {
                { "real", "realistic fantasy world map" },
                { "fantasy", "fantasy medieval map with castles and forests" },
                { "fortnite", "colorful cartoon island battle royale style map" },
                { "minecraft", "blocky pixel art minecraft style terrain map" },
                { "gta", "open world city map similar to GTA games" }
            };

            var styleName = styleNames.GetValueOrDefault(request.Style, "fantasy world map");
            var prompt = $"Generate a detailed {styleName} for RPG campaign named '{request.CampaignName}'. Top-down view, ancient parchment style if fantasy, clean lines, various landmarks, mountains, forests, rivers, villages, castles. No text. High quality.";

            var imageUrl = $"https://image.pollinations.ai/prompt/{Uri.EscapeDataString(prompt)}?width=1024&height=1024&nologo=true";

            campaign.MapUrl = imageUrl;
            campaign.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new JsonResult(new { imageUrl = imageUrl });
        }

        public async Task<IActionResult> OnPostUploadMapAsync(IFormFile file, int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return Forbid();

            if (file == null || file.Length == 0)
                return BadRequest("No file provided");

            var campaign = await _context.Campaigns.FindAsync(campaignId);
            if (campaign == null)
                return NotFound();

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            campaign.MapData = Convert.ToBase64String(imageBytes);
            campaign.MapContentType = file.ContentType;
            campaign.MapUrl = null;
            campaign.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        public class AddLocationRequest
        {
            public int CampaignId { get; set; }
            public string Name { get; set; } = "";
            public string? Description { get; set; }
            public string? Region { get; set; }
            public string? Type { get; set; }
            public double MapX { get; set; }
            public double MapY { get; set; }
        }

        public class GenerateMapRequest
        {
            public int CampaignId { get; set; }
            public string CampaignName { get; set; } = "";
            public string Style { get; set; } = "fantasy";
        }
    }
}
