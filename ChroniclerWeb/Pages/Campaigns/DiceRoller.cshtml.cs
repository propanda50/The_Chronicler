using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChroniclerWeb.Pages.Campaigns
{
    [Authorize]
    public class DiceRollerModel : PageModel
    {
        public int CampaignId { get; set; }

        public IActionResult OnGet(int campaignId)
        {
            CampaignId = campaignId;
            return Page();
        }
    }
}
