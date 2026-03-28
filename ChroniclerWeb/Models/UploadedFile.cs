
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChroniclerWeb.Models
{
    public enum FileType
    {
        Document,
        Image,
        Audio,
        Map,
        CharacterPortrait,
        CampaignLogo,
        Other
    }

    public class UploadedFile
    {
        public int Id { get; set; }

        [Required, MaxLength(500)]
        public string FileName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        [MaxLength(1000)]
        public string? Url { get; set; }

        public string? FileData { get; set; }

        public FileType Type { get; set; } = FileType.Other;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string UploadedById { get; set; } = string.Empty;

        [ForeignKey(nameof(UploadedById))]
        public ApplicationUser UploadedBy { get; set; } = null!;

        public int? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public int? CharacterId { get; set; }
        public Character? Character { get; set; }

        public int? LocationId { get; set; }
        public Location? Location { get; set; }

        public int? EventId { get; set; }
        public Event? Event { get; set; }

        public int? SessionId { get; set; }
        public Session? Session { get; set; }

        public int? ForumPostId { get; set; }
        public ForumPost? ForumPost { get; set; }
    }
}
