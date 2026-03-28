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
    [IgnoreAntiforgeryToken]
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
        public string? MapUrl { get; set; }
        public List<LocationDto> Locations { get; set; } = new();

        public class LocationDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Type { get; set; } = "";
            public double? MapX { get; set; }
            public double? MapY { get; set; }
        }

        public class AddLocationRequest
        {
            public int CampaignId { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Region { get; set; } = "";
            public string Type { get; set; } = "";
            public double? MapX { get; set; }
            public double? MapY { get; set; }
        }

        public class UpdateLocationRequest
        {
            public int Id { get; set; }
            public int CampaignId { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public string Region { get; set; } = "";
            public string Type { get; set; } = "";
            public double? MapX { get; set; }
            public double? MapY { get; set; }
        }

        public class DeleteLocationRequest
        {
            public int Id { get; set; }
            public int CampaignId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserAccess(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            await LoadDataAsync(campaignId);
            return Page();
        }

        public async Task<IActionResult> OnPostAddLocationAsync([FromBody] AddLocationRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(request.CampaignId, userId))
                return new JsonResult(new { success = false, error = "Access denied." });

            if (string.IsNullOrWhiteSpace(request.Name))
                return new JsonResult(new { success = false, error = "Name is required." });

            var location = new Location
            {
                CampaignId = request.CampaignId,
                Name = request.Name.Trim(),
                Description = request.Description?.Trim() ?? "",
                Region = request.Region?.Trim() ?? "",
                Type = request.Type?.Trim() ?? "",
                MapX = request.MapX,
                MapY = request.MapY,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, id = location.Id });
        }

        public async Task<IActionResult> OnPostUpdateLocationAsync([FromBody] UpdateLocationRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(request.CampaignId, userId))
                return new JsonResult(new { success = false, error = "Access denied." });

            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == request.Id && l.CampaignId == request.CampaignId);

            if (location == null)
                return new JsonResult(new { success = false, error = "Location not found." });

            location.Name = request.Name.Trim();
            location.Description = request.Description?.Trim() ?? "";
            location.Type = request.Type?.Trim() ?? "";
            location.Region = request.Region?.Trim() ?? "";
            location.MapX = request.MapX;
            location.MapY = request.MapY;
            location.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostDeleteLocationAsync([FromBody] DeleteLocationRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(request.CampaignId, userId))
                return new JsonResult(new { success = false, error = "Access denied." });

            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == request.Id && l.CampaignId == request.CampaignId);

            if (location == null)
                return new JsonResult(new { success = false, error = "Location not found." });

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostUploadMapAsync(IFormFile file, int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return new JsonResult(new { success = false, error = "Access denied." });

            if (file == null || file.Length == 0)
                return new JsonResult(new { success = false, error = "No file." });

            var campaign = await _context.Campaigns.FindAsync(campaignId);
            if (campaign == null)
                return new JsonResult(new { success = false, error = "Campaign not found." });

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            campaign.MapData = Convert.ToBase64String(ms.ToArray());
            campaign.MapContentType = file.ContentType;
            campaign.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }

        private async Task LoadDataAsync(int campaignId)
        {
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            CampaignId = campaignId;

            if (!string.IsNullOrEmpty(campaign?.MapData))
            {
                var contentType = campaign.MapContentType ?? "image/jpeg";
                MapUrl = campaign.MapData.StartsWith("data:")
                    ? campaign.MapData
                    : $"data:{contentType};base64,{campaign.MapData}";
            }
            else if (!string.IsNullOrEmpty(campaign?.MapUrl))
            {
                MapUrl = campaign.MapUrl;
            }

            Locations = await _context.Locations
                .Where(l => l.CampaignId == campaignId)
                .OrderBy(l => l.Name)
                .Select(l => new LocationDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Type = l.Type,
                    MapX = l.MapX,
                    MapY = l.MapY
                })
                .ToListAsync();
        }
    }
}
