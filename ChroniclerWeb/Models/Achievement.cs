using System;
using System.ComponentModel.DataAnnotations;

public enum AchievementType
{
    FirstTime,      // One-time achievements
    Progress,       // Progress-based (e.g., 10, 50, 100)
    Milestone,      // Major milestones
    Secret          // Hidden achievements
}

public class Achievement
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = "";

    [MaxLength(500)]
    public string Description { get; set; } = "";

    [MaxLength(50)]
    public string Icon { get; set; } = "🏆";

    public AchievementType Type { get; set; } = AchievementType.FirstTime;

    public int XP { get; set; } = 50;

    [MaxLength(100)]
    public string Category { get; set; } = "General";

    public int? RequiredCount { get; set; } // For progress achievements

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class PlayerAchievement
{
    public int Id { get; set; }

    [Required]
    public string PlayerId { get; set; } = "";

    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; } = null!;

    public int CurrentProgress { get; set; } = 0;
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime FirstProgressAt { get; set; } = DateTime.UtcNow;
}

// Predefined achievements
public static class AchievementList
{
    public static readonly (string Name, string Desc, string Icon, string Category, int XP, int? Required)[] All =
    {
            // First Steps
            ("First Steps", "Create your first character", "🎯", "Getting Started", 50, null),
            ("Campaign Starter", "Create your first campaign", "📚", "Getting Started", 100, null),
            ("Group Found", "Join a campaign as a player", "🤝", "Getting Started", 75, null),
            
            // Combat
            ("First Blood", "Win your first combat", "⚔️", "Combat", 100, null),
            ("Monster Hunter", "Defeat 10 enemies", "🗡️", "Combat", 150, 10),
            ("Dragon Slayer", "Defeat a dragon", "🐉", "Combat", 500, null),
            ("Survivor", "Win 10 combats", "🛡️", "Combat", 300, 10),
            
            // Roleplay
            ("Roleplayer", "Write 5000 words of backstory", "📜", "Roleplay", 200, 5000),
            ("Lorekeeper", "Create 10 locations", "🗺️", "Roleplay", 150, 10),
            ("Storyteller", "Complete 5 sessions", "📖", "Roleplay", 200, 5),
            
            // Social
            ("Team Player", "Play in a party of 4+ characters", "👥", "Social", 100, null),
            ("Friends Forever", "Create 5 characters in same campaign", "💕", "Social", 150, 5),
            
            // Completion
            ("Campaign Complete", "Finish an entire campaign", "🏆", "Completion", 1000, null),
            ("Completionist", "Unlock 20 achievements", "⭐", "Completion", 500, 20),
            ("Veteran", "Play 50 sessions", "🎖️", "Completion", 1000, 50),
            
            // Special
            ("Night Owl", "Play after midnight", "🦉", "Special", 50, null),
            ("Early Bird", "Play before 6 AM", "🌅", "Special", 50, null),
            ("Dice Roller", "Roll 100 dice", "🎲", "Special", 100, 100),
            ("Lucky Seven", "Roll a natural 7 three times in a row", "🍀", "Special", 200, null),
            ("Nat 20!", "Roll a natural 20", "✨", "Special", 75, null)
        };
}