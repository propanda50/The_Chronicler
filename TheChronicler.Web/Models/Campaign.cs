using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheChronicler.Web.Models
{
    public enum CampaignStatus
    {
        Active,
        Paused,
        Completed
    }

    public class Campaign
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string GameSystem { get; set; } = string.Empty;

        public CampaignStatus Status { get; set; } = CampaignStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Owner
        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; } = null!;

        // Navigation properties
        public ICollection<CampaignMember> Members { get; set; } = new List<CampaignMember>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
