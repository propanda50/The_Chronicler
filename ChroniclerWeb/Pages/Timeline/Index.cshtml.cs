using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Timeline
{
    public class TimelineEntry
    {
        public string Type { get; set; } = "session";
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public DateTime Date { get; set; }
        public int SessionNumber { get; set; }
        public bool HasKeyEvent { get; set; }
        public List<CharacterRef> Characters { get; set; } = new();
        public List<LocationRef> Locations { get; set; } = new();
        public List<EventRef> Events { get; set; } = new();
    }

    public class CharacterRef
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class LocationRef
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class EventRef
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsKeyEvent { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICampaignService _campaignService;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICampaignService campaignService)
        {
            _context = context;
            _userManager = userManager;
            _campaignService = campaignService;
        }

        public Campaign Campaign { get; set; } = null!;
        public List<Session> Sessions { get; set; } = new();
        public List<Event> KeyEvents { get; set; } = new();
        public List<TimelineEntry> TimelineEntries { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int campaignId)
        {
            var userId = _userManager.GetUserId(User)!;
            if (!await _campaignService.IsUserMember(campaignId, userId))
                return RedirectToPage("/Account/AccessDenied");

            Campaign = (await _context.Campaigns.FindAsync(campaignId))!;
            if (Campaign == null) return NotFound();

            Sessions = await _context.Sessions
                .Include(s => s.SessionCharacters).ThenInclude(sc => sc.Character)
                .Include(s => s.SessionLocations).ThenInclude(sl => sl.Location)
                .Include(s => s.Events)
                .Where(s => s.CampaignId == campaignId)
                .OrderBy(s => s.SessionNumber)
                .ToListAsync();

            KeyEvents = await _context.Events
                .Where(e => e.CampaignId == campaignId && e.IsKeyEvent)
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            // Build timeline entries
            foreach (var session in Sessions)
            {
                var entry = new TimelineEntry
                {
                    Type = "session",
                    Id = session.Id,
                    Title = session.Title,
                    Summary = session.Summary,
                    Date = session.SessionDate,
                    SessionNumber = session.SessionNumber,
                    HasKeyEvent = session.Events.Any(e => e.IsKeyEvent),
                    Characters = session.SessionCharacters.Select(sc => new CharacterRef
                    {
                        Id = sc.Character.Id,
                        Name = sc.Character.Name
                    }).ToList(),
                    Locations = session.SessionLocations.Select(sl => new LocationRef
                    {
                        Id = sl.Location.Id,
                        Name = sl.Location.Name
                    }).ToList(),
                    Events = session.Events.Select(e => new EventRef
                    {
                        Id = e.Id,
                        Title = e.Title,
                        IsKeyEvent = e.IsKeyEvent
                    }).ToList()
                };

                TimelineEntries.Add(entry);
            }

            return Page();
        }
    }
}
