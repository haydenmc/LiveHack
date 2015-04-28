using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LiveHackDb.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map one-to-many from team-to-user
            // Chat owners many-to-many table
            modelBuilder.Entity<Chat>().HasMany(c => c.Owners).WithMany(u => u.ChatsOwned).Map(m =>
            {
                m.MapLeftKey("ChatId");
                m.MapRightKey("UserId");
                m.ToTable("ChatOwners");
            });

            // Chat users many-to-many table
            modelBuilder.Entity<Chat>().HasMany(c => c.Users).WithMany(u => u.Chats).Map(m =>
            {
                m.MapLeftKey("ChatId");
                m.MapRightKey("UserId");
                m.ToTable("ChatUsers");
            });
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
    }
}