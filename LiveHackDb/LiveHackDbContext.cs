using LiveHackDb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHackDb
{
	public class LiveHackDbContext : IdentityDbContext<User>
	{
		public LiveHackDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static LiveHackDbContext Create()
		{
			return new LiveHackDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Institution>().HasOptional(i => i.Group).WithOptionalPrincipal(g => g.Institution);
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Hackathon> Hackathons { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Institution> Institutions { get; set; }
		public DbSet<Technology> Technologies { get; set; }
		public DbSet<Message> Messages { get; set; }
	}
}
