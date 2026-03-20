using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheChronicler.Web.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Region { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Type { get; set; } = string.Empty; // City, Dungeon, Tavern, etc.

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Campaign
        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        // Navigation
        public ICollection<SessionLocation> SessionLocations { get; set; } = new List<SessionLocation>();
    }
}
