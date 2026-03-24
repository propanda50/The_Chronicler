using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;
using ChroniclerWeb.Services.FileUpload;


namespace ChroniclerWeb.Pages.Gallery
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileUploadService _fileUploadService;
        private readonly ICampaignService _campaignService;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IFileUploadService fileUploadService,
            ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _fileUploadService = fileUploadService;
            _campaignService = campaignService;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public List<UploadedFile> Images { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";

            Images = await _context.UploadedFiles
                .Where(f => f.CampaignId == campaignId && 
                       (f.ContentType.StartsWith("image/") || f.Type == FileType.Map))
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostUploadAsync(int campaignId, FileType fileType)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            var files = Request.Form.Files;
            foreach (var file in files)
            {
                if (file.Length > 0 && file.ContentType.StartsWith("image/"))
                {
                    await _fileUploadService.UploadFileAsync(file, userId, campaignId, fileType: fileType);
                }
            }

            TempData["Success"] = $"{files.Count} image(s) uploaded successfully!";
            return RedirectToPage(new { campaignId });
        }
    }
}
