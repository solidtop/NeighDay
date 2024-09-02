using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeighDay.Server.Features.Avatars;
using NeighDay.Server.Features.Chats;
using NeighDay.Server.Features.Chats.Channels;
using NeighDay.Server.Features.Users;

namespace NeighDay.Server.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatChannel> ChatChannels { get; set; }
        public DbSet<Avatar> Avatars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatMessage>()
                .HasOne(cm => cm.Channel)
                .WithMany()
                .HasForeignKey(cm => cm.ChannelId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatMessage>()
                .HasOne(cm => cm.User)
                .WithMany()
                .HasForeignKey(cm => cm.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>().Navigation(e => e.Avatar).AutoInclude();

            builder.Entity<ChatMessage>().Navigation(e => e.Channel).AutoInclude();
            builder.Entity<ChatMessage>().Navigation(e => e.User).AutoInclude();

            builder.Entity<ChatChannel>().HasData(
                new ChatChannel { Id = 1, Name = "global" },
                new ChatChannel { Id = 2, Name = "trade" }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "player", NormalizedName = "PLAYER" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "dev", NormalizedName = "DEV" }
            );

            builder.Entity<Avatar>().HasData(
                new Avatar { Id = 1, Name = "Avatar1", ImageUrl = "/assets/avatars/marble-avatar.png" }
            );
        }
    }
}
