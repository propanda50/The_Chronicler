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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Campaign
            builder.Entity<Campaign>(entity =>
            {
                entity.HasIndex(c => c.OwnerId);
                entity.HasIndex(c => c.Status);

                entity.HasOne(c => c.Owner)
                    .WithMany(u => u.OwnedCampaigns)
                    .HasForeignKey(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // CampaignMember
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

            // Session
            builder.Entity<Session>(entity =>
            {
                entity.HasIndex(s => new { s.CampaignId, s.SessionNumber }).IsUnique();

                entity.HasOne(s => s.Campaign)
                    .WithMany(c => c.Sessions)
                    .HasForeignKey(s => s.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Character
            builder.Entity<Character>(entity =>
            {
                entity.HasIndex(ch => ch.CampaignId);

                entity.HasOne(ch => ch.Campaign)
                    .WithMany(c => c.Characters)
                    .HasForeignKey(ch => ch.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Location
            builder.Entity<Location>(entity =>
            {
                entity.HasIndex(l => l.CampaignId);

                entity.HasOne(l => l.Campaign)
                    .WithMany(c => c.Locations)
                    .HasForeignKey(l => l.CampaignId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Event
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

            // SessionCharacter (join table)
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

            // SessionLocation (join table)
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
        }
    }
}
