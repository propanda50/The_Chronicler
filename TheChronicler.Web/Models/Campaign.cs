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

    public enum RecruitmentStatus
    {
        Closed,
        LookingForPlayers,
        LookingForGameMaster,
        ReadyToPlay
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

        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        public string? LogoData { get; set; }

        public CampaignStatus Status { get; set; } = CampaignStatus.Active;
        public RecruitmentStatus RecruitmentStatus { get; set; } = RecruitmentStatus.Closed;

        [MaxLength(1000)]
        public string? RecruitmentDescription { get; set; }

        public int MaxPlayers { get; set; } = 6;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser Owner { get; set; } = null!;

        public ICollection<CampaignMember> Members { get; set; } = new List<CampaignMember>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Character> Characters { get; set; } = new List<Character>();
        public ICollection<Location> Locations { get; set; } = new List<Location>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
    }
}
