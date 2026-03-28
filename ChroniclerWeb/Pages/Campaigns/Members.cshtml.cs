using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChroniclerWeb.Data;
using ChroniclerWeb.Models;
using ChroniclerWeb.Services;

namespace ChroniclerWeb.Pages.Campaigns
{
    public class MembersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public MembersModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public Campaign Campaign { get; set; } = null!;
        public List<CampaignMember> Members { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserOwner(id, userId))
                return RedirectToPage("/Account/AccessDenied");

            Campaign = (await _context.Campaigns
                .Include(c => c.Owner)
                .FirstOrDefaultAsync(c => c.Id == id))!;

            if (Campaign == null) return NotFound();

            Members = await _context.CampaignMembers
                .Include(m => m.User)
                .Where(m => m.CampaignId == id && m.UserId != Campaign.OwnerId)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddMemberAsync(int campaignId, string pseudo, string role, bool canAddNotes)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserOwner(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            var normalizedPseudo = pseudo.Trim().ToUpperInvariant();

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedPseudo == normalizedPseudo);

            if (user == null)
            {
                TempData["Error"] = $"No user found with pseudo '{pseudo}'.";
                return RedirectToPage(new { id = campaignId });
            }

            var existing = await _context.CampaignMembers
                .AnyAsync(m => m.CampaignId == campaignId && m.UserId == user.Id);

            if (existing)
            {
                TempData["Error"] = "This user is already a member of this campaign.";
                return RedirectToPage(new { id = campaignId });
            }

            if (!Enum.TryParse<CampaignRole>(role, ignoreCase: true, out var parsedRole))
            {
                TempData["Error"] = "Invalid role specified.";
                return RedirectToPage(new { id = campaignId });
            }

            var member = new CampaignMember
            {
                CampaignId = campaignId,
                UserId = user.Id,
                Role = parsedRole,
                CanAddNotes = canAddNotes
            };

            _context.CampaignMembers.Add(member);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"{user.DisplayName} added to the campaign!";
            return RedirectToPage(new { id = campaignId });
        }

        public async Task<IActionResult> OnPostRemoveMemberAsync(int campaignId, int memberId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserOwner(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            var member = await _context.CampaignMembers.FindAsync(memberId);
            if (member != null)
            {
                _context.CampaignMembers.Remove(member);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Member removed.";
            }

            return RedirectToPage(new { id = campaignId });
        }
    }
}
