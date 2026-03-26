using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Models;

namespace TheChronicler.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<CampaignMember> CampaignMembers => Set<CampaignMember>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<SessionCharacter> SessionCharacters => Set<SessionCharacter>();
        public DbSet<SessionLocation> SessionLocations => Set<SessionLocation>();
        public DbSet<ForumPost> ForumPosts => Set<ForumPost>();
        public DbSet<ForumReply> ForumReplies => Set<ForumReply>();
        public DbSet<UploadedFile> UploadedFiles => Set<UploadedFile>();
        public DbSet<Achievement> Achievements => Set<Achievement>();
        public DbSet<PlayerAchievement> PlayerAchievements => Set<PlayerAchievement>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Campaign>(entity =>
            {
                entity.HasIndex(c => c.OwnerId);
                entity.HasIndex(c => c.Status);

                entity.HasOne(c => c.Owner)
                    .WithMany(u => u.OwnedCampaigns)
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CampaignMember>(entity =>
            {
                entity.HasIndex(cm => new { cm.CampaignId, cm.UserId }).IsUnique();

                entity.HasOne(cm => cm.Campaign)
                    .WithMany(c => c.Members)
                    .HasForeignKey(cm => cm.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cm => cm.User)
                    .WithMany(u => u.CampaignMemberships)
                    .HasForeignKey(cm => cm.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Session>(entity =>
            {
                entity.HasIndex(s => new { s.CampaignId, s.SessionNumber }).IsUnique();

                entity.HasOne(s => s.Campaign)
                    .WithMany(c => c.Sessions)
                    .HasForeignKey(s => s.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Character>(entity =>
            {
                entity.HasIndex(ch => ch.CampaignId);

                entity.HasOne(ch => ch.Campaign)
                    .WithMany(c => c.Characters)
                    .HasForeignKey(ch => ch.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Location>(entity =>
            {
                entity.HasIndex(l => l.CampaignId);

                entity.HasOne(l => l.Campaign)
                    .WithMany(c => c.Locations)
                    .HasForeignKey(l => l.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Event>(entity =>
            {
                entity.HasIndex(e => e.CampaignId);
                entity.HasIndex(e => e.IsKeyEvent);

                entity.HasOne(e => e.Campaign)
                    .WithMany(c => c.Events)
                    .HasForeignKey(e => e.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Session)
                    .WithMany(s => s.Events)
                    .HasForeignKey(e => e.SessionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<SessionCharacter>(entity =>
            {
                entity.HasIndex(sc => new { sc.SessionId, sc.CharacterId }).IsUnique();

                entity.HasOne(sc => sc.Session)
                    .WithMany(s => s.SessionCharacters)
                    .HasForeignKey(sc => sc.SessionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sc => sc.Character)
                    .WithMany(c => c.SessionCharacters)
                    .HasForeignKey(sc => sc.CharacterId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SessionLocation>(entity =>
            {
                entity.HasIndex(sl => new { sl.SessionId, sl.LocationId }).IsUnique();

                entity.HasOne(sl => sl.Session)
                    .WithMany(s => s.SessionLocations)
                    .HasForeignKey(sl => sl.SessionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sl => sl.Location)
                    .WithMany(l => l.SessionLocations)
                    .HasForeignKey(sl => sl.LocationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ForumPost>(entity =>
            {
                entity.HasIndex(fp => fp.CampaignId);
                entity.HasIndex(fp => fp.AuthorId);

                entity.HasOne(fp => fp.Campaign)
                    .WithMany(c => c.ForumPosts)
                    .HasForeignKey(fp => fp.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fp => fp.Author)
                    .WithMany()
                    .HasForeignKey(fp => fp.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ForumReply>(entity =>
            {
                entity.HasIndex(fr => fr.PostId);
                entity.HasIndex(fr => fr.AuthorId);

                entity.HasOne(fr => fr.Post)
                    .WithMany(p => p.Replies)
                    .HasForeignKey(fr => fr.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(fr => fr.Author)
                    .WithMany()
                    .HasForeignKey(fr => fr.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UploadedFile>(entity =>
            {
                entity.HasIndex(uf => uf.UploadedById);
                entity.HasIndex(uf => uf.CampaignId);
                entity.HasIndex(uf => uf.CharacterId);

                entity.HasOne(uf => uf.UploadedBy)
                    .WithMany()
                    .HasForeignKey(uf => uf.UploadedById)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uf => uf.Campaign)
                    .WithMany()
                    .HasForeignKey(uf => uf.CampaignId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(uf => uf.Character)
                    .WithMany()
                    .HasForeignKey(uf => uf.CharacterId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(uf => uf.Location)
                    .WithMany()
                    .HasForeignKey(uf => uf.LocationId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Achievement>(entity =>
            {
                entity.HasIndex(a => a.Name).IsUnique();
            });

            builder.Entity<PlayerAchievement>(entity =>
            {
                entity.HasIndex(pa => new { pa.PlayerId, pa.AchievementId }).IsUnique();

                entity.HasOne(pa => pa.Achievement)
                    .WithMany()
                    .HasForeignKey(pa => pa.AchievementId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
