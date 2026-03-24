using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Files
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
        public IEnumerable<UploadedFile> Files { get; set; } = Enumerable.Empty<UploadedFile>();

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            var campaign = await _context.Campaigns.FindAsync(campaignId);
            CampaignName = campaign?.Name ?? "";
            Files = await _fileUploadService.GetFilesByCampaignAsync(campaignId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file, int campaignId, FileType fileType)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            if (file != null && file.Length > 0)
            {
                await _fileUploadService.UploadFileAsync(file, userId, campaignId, fileType: fileType);
                TempData["Success"] = "File uploaded successfully!";
            }

            return RedirectToPage(new { campaignId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int fileId, int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            await _fileUploadService.DeleteFileAsync(fileId);
            TempData["Success"] = "File deleted.";

            return RedirectToPage(new { campaignId });
        }
    }
}
