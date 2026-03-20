using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheChronicler.Web.Models
{
    public enum CharacterStatus
    {
        Alive,
        Dead,
        Missing,
        Unknown
    }

    public enum CharacterType
    {
        PlayerCharacter,
        NPC,
        Villain,
        Ally
    }

    public class Character
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Role { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Race { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Class { get; set; } = string.Empty;

        public CharacterStatus Status { get; set; } = CharacterStatus.Alive;
        public CharacterType Type { get; set; } = CharacterType.NPC;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Campaign
        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        // Navigation
        public ICollection<SessionCharacter> SessionCharacters { get; set; } = new List<SessionCharacter>();
    }
}
