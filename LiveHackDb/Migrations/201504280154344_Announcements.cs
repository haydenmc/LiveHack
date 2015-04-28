namespace LiveHackDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        AnnouncementId = c.Guid(nullable: false),
                        Title = c.String(),
                        Body = c.String(),
                        DateTimeCreated = c.DateTimeOffset(nullable: false, precision: 7),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AnnouncementId)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            AddColumn("dbo.AspNetUsers", "IsOrganizer", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.AspNetUsers", "IsSponsor", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Announcements", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Announcements", new[] { "Author_Id" });
            DropColumn("dbo.AspNetUsers", "IsSponsor");
            DropColumn("dbo.AspNetUsers", "IsOrganizer");
            DropTable("dbo.Announcements");
        }
    }
}
