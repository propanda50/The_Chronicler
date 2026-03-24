using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerData.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public int SessionNumber { get; set; }

        public DateTime SessionDate { get; set; } = DateTime.UtcNow;

        // Rich text content (HTML from editor)
        public string Notes { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Summary { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Campaign
        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        // Navigation properties
        public ICollection<SessionCharacter> SessionCharacters { get; set; } = new List<SessionCharacter>();
        public ICollection<SessionLocation> SessionLocations { get; set; } = new List<SessionLocation>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
