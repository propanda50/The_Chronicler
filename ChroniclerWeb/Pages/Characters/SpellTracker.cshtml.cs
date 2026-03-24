using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

namespace TheChronicler.Web.Pages.Characters
{
    [Authorize]
    public class SpellTrackerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SpellTrackerModel(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Character Character { get; set; } = new();
        public int CharacterLevel { get; set; } = 1;
        public string CharacterClass { get; set; } = "Wizard";
        public List<SpellDto> Spells { get; set; } = new();

        public class SpellDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Level { get; set; }
            public string School { get; set; } = "";
            public string CastingTime { get; set; } = "1 action";
            public string Range { get; set; } = "30 feet";
            public List<string> Components { get; set; } = new();
            public bool Concentration { get; set; }
            public string Description { get; set; } = "";
        }

        public async Task<IActionResult> OnGetAsync(int characterId)
        {
            var userId = _userManager.GetUserId(User)!;
            Character = await _context.Characters.FindAsync(characterId) ?? new Character();

            if (Character.Id == 0)
                return NotFound();

            // For demo, using Wizard spell list
            CharacterClass = Character.Class ?? "Wizard";
            CharacterLevel = 5; // Would be from character data

            Spells = GetSampleSpells();

            return Page();
        }

        public int GetSlotsForLevel(int spellLevel)
        {
            // Basic spell slot table for a level 5 Wizard
            var slots = new Dictionary<int, int>
            {
                { 1, 4 }, { 2, 3 }, { 3, 2 }, { 4, 1 }
            };
            return slots.GetValueOrDefault(spellLevel, 0);
        }

        private List<SpellDto> GetSampleSpells()
        {
            return new List<SpellDto>
            {
                new() { Id = 1, Name = "Fire Bolt", Level = 0, School = "Evocation", Components = new() { "V", "S" }, Description = "You hurl a mote of fire at a creature or object." },
                new() { Id = 2, Name = "Mage Hand", Level = 0, School = "Conjuration", Components = new() { "V", "S" }, Description = "A spectral, floating hand appears." },
                new() { Id = 3, Name = "Shield", Level = 1, School = "Abjuration", Components = new() { "V", "S" }, CastingTime = "1 reaction", Description = "+5 bonus to AC until start of your next turn." },
                new() { Id = 4, Name = "Magic Missile", Level = 1, School = "Evocation", Components = new() { "V", "S" }, Description = "Create three glowing darts that automatically hit." },
                new() { Id = 5, Name = "Mirror Image", Level = 2, School = "Illusion", Components = new() { "V", "S" }, Concentration = false, Description = "Three illusory duplicates of yourself appear." },
                new() { Id = 6, Name = "Scorching Ray", Level = 2, School = "Evocation", Components = new() { "V", "S" }, Description = "Create three rays of fire and hurl them at targets." },
                new() { Id = 7, Name = "Counterspell", Level = 3, School = "Abjuration", Components = new() { "S" }, CastingTime = "1 reaction", Description = "Attempt to interrupt a creature casting a spell." },
                new() { Id = 8, Name = "Fireball", Level = 3, School = "Evocation", Components = new() { "V", "S", "M" }, Description = "A bright streak flashes to a point and explodes in a sphere of fire." },
                new() { Id = 9, Name = "Haste", Level = 3, School = "Transmutation", Components = new() { "V", "S", "M" }, Concentration = true, Description = "Choose a willing creature to gain +2 AC, double speed, and advantage on DEX saves." },
                new() { Id = 10, Name = "Counterspell", Level = 3, School = "Abjuration", Components = new() { "S" }, Description = "Attempt to counter another spell." },
            };
        }
    }
}
