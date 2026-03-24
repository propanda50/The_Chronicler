using ChroniclerData.Models;
using Fluent.Infrastructure.FluentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public enum ForumCategory
    {
        General,
        LookingForGroup,
        Recruitment,
        TipsAndHelp,
        Discussion
    }

    public class ForumPost
    {
        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(10000)]
        public string Content { get; set; } = string.Empty;

        public ForumCategory Category { get; set; } = ForumCategory.General;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public Campaign Campaign { get; set; } = null!;

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;

        public ICollection<ForumReply> Replies { get; set; } = new List<ForumReply>();
    }

    public class ForumReply
    {
        public int Id { get; set; }

        [Required, MaxLength(5000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public ForumPost Post { get; set; } = null!;

        [Required]
        public string AuthorId { get; set; } = string.Empty;

        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;
    }

