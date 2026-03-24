using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.AIGM
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;
        private readonly IAIGameMasterService _aiGM;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            ICampaignService campaignService,
            IAIGameMasterService aiGM)
        {
            _userManager = userManager;
            _campaignService = campaignService;
            _aiGM = aiGM;
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = "";

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            CampaignId = campaignId;
            return Page();
        }

        public async Task<IActionResult> OnPostChatAsync([FromBody] ChatRequest request)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.CanUserEdit(request.CampaignId, userId))
                return Forbid();

            var response = await _aiGM.GenerateNPCResponseAsync(
                request.NpcName,
                request.NpcPersonality,
                request.PlayerMessage,
                $"Campaign ID: {request.CampaignId}");

            return new JsonResult(new { response });
        }

        public class ChatRequest
        {
            public int CampaignId { get; set; }
            public string NpcName { get; set; } = "";
            public string NpcPersonality { get; set; } = "";
            public string PlayerMessage { get; set; } = "";
        }
    }
}
