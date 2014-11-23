namespace LiveHackDb.Migrations
{
	using LiveHackDb.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<LiveHackDb.LiveHackDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LiveHackDb.LiveHackDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

			context.Hackathons.Add(new Hackathon()
			{
				HackathonId = Guid.NewGuid(),
				Name = "Wildhacks 2014",
				ShortName = "wildhacks",
				Description="Wildhacks 2014 yay hooray",
				Groups = new List<HackathonGroup>()
				{
					new HackathonGroup() {
						GroupId = Guid.NewGuid(),
						Description = "Main hackathon group",
						Name = "Main"
					}
				},
				StartDateTime= DateTimeOffset.Now,
				EndDateTime=DateTimeOffset.Now.Add(TimeSpan.FromDays(2)),
				Institution = new Institution()
				{
					InstitutionId=Guid.NewGuid(),
					Name="Northwestern University",
					Group = new InstitutionGroup()
					{
						GroupId=Guid.NewGuid(),
						Description="Group for Northwestern University",
						Name="Northwestern University Group"
					}
				}
			});
        }
    }
}
