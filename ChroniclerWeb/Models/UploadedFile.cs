using ChroniclerData.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Required, MaxLength(50)]
    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    [MaxLength(500)]
    public string? Url { get; set; }

    public string? FileData { get; set; }

    public FileType Type { get; set; } = FileType.Other;

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public string UploadedById { get; set; } = string.Empty;

    [ForeignKey(nameof(UploadedById))]
    public ApplicationUser UploadedBy { get; set; } = null!;

    public int? CampaignId { get; set; }

    [ForeignKey(nameof(CampaignId))]
    public Campaign? Campaign { get; set; }

    public int? CharacterId { get; set; }

    [ForeignKey(nameof(CharacterId))]
    public Character? Character { get; set; }

    public int? LocationId { get; set; }

    [ForeignKey(nameof(LocationId))]
    public Location? Location { get; set; }
}
