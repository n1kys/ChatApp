using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Emit;

namespace ChatApp.DataAccess.Data
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Chat> Chats { get; set; }
            public DbSet<Message> Messages { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity(j => j.ToTable("ChatUser"));

            modelBuilder.Entity<Message>()
                    .HasOne(m => m.Chat)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(m => m.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Message>()
                    .HasOne(m => m.User)
                    .WithMany(u => u.Messages)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Restrict); 

                modelBuilder.Entity<Chat>()
                    .HasOne(c => c.CreatedByUser)
                    .WithMany(u => u.CreatedChats)
                    .HasForeignKey(c => c.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict); 


                base.OnModelCreating(modelBuilder);
            }

        }
}
