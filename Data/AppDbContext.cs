using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkChat.Models;

namespace WorkChat.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<ChatRoom> ChatRooms { get; set; } = null!;
        public DbSet<ChatRoomParticipant> ChatRoomsParticipants { get; set;} = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // CHATROOM

            builder.Entity<ChatRoom>(entity =>
            {
                entity.HasKey(cr => cr.Id);

                entity.Property(cr => cr.Name)
                    .HasMaxLength(100);

                entity.HasOne(cr => cr.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(cr => cr.CreatedByUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // CHATROOM PARTICIPANT (JOIN TABLE)
            // Composite key: ChatRoomId + UserId

            builder.Entity<ChatRoomParticipant>(entity =>
            {
                entity.HasKey(cp => new { cp.ChatRoomId, cp.UserId });

                entity.HasOne(cp => cp.ChatRoom)
                      .WithMany(cr => cr.Participants)
                      .HasForeignKey(cp => cp.ChatRoomId);

                entity.HasOne(cp => cp.User)
                      .WithMany()
                      .HasForeignKey(cp => cp.UserId);
            });

            // MESSAGES

            builder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Text)
                      .IsRequired();

                entity.HasOne(m => m.ChatRoom)
                      .WithMany(cr => cr.Messages)
                      .HasForeignKey(m => m.ChatRoomId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Sender)
                      .WithMany()
                      .HasForeignKey(m => m.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
