using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChroniclerWeb.Models
{
    public enum CharacterStatus
    {
        Alive,
        Dead,
        Undead,
        Missing,
        Unknown
    }

    public enum CharacterType
    {
        Player,
        NPC
    }

    public enum NPCRoles
    {
        None,
        Antagonist,
        Ally,
        Merchant,
        PlotDriver,
        QuestGiver,
        Informant,
        Henchman
    }

    public class Character
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Role { get; set; }

        [MaxLength(100)]
        public string Race { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Class { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? PortraitUrl { get; set; }

        public string? PortraitData { get; set; }
        public string? PortraitContentType { get; set; } = "image/svg+xml";

        public CharacterStatus Status { get; set; } = CharacterStatus.Alive;
        public CharacterType Type { get; set; } = CharacterType.Player;
        public NPCRoles NPCRole { get; set; } = NPCRoles.None;

        public int Strength { get; set; } = 0;
        public int Agility { get; set; } = 0;
        public int Health { get; set; } = 0;
        public int Intelligence { get; set; } = 0;

        // Keeping these for backward compatibility with existing data and services
        // These are not used in the new stat system but kept to avoid breaking changes
        public int Dexterity { get; set; } = 0;
        public int Constitution { get; set; } = 0;
        public int Wisdom { get; set; } = 0;
        public int Charisma { get; set; } = 0;

        [NotMapped]
        public int MaxStatPoints { get; set; } = 54;

        [NotMapped]
        public int SpentPoints => Strength + Agility + Health + Intelligence;

        [NotMapped]
        public int RemainingPoints => MaxStatPoints - SpentPoints;

        [MaxLength(2000)]
        public string? GMNotes { get; set; }

        [MaxLength(2000)]
        public string? SharedNotes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        public ICollection<SessionCharacter> SessionCharacters { get; set; } = new List<SessionCharacter>();
    }
}
