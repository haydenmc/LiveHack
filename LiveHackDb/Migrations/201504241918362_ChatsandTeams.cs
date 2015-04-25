namespace LiveHackDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatsandTeams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccessCode = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        DateTimeCreated = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Messages", "Team_Id", c => c.Guid());
            AddColumn("dbo.AspNetUsers", "Team_Id", c => c.Guid());
            CreateIndex("dbo.Messages", "Team_Id");
            CreateIndex("dbo.AspNetUsers", "Team_Id");
            AddForeignKey("dbo.Messages", "Team_Id", "dbo.Teams", "Id");
            AddForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Messages", "Team_Id", "dbo.Teams");
            DropIndex("dbo.AspNetUsers", new[] { "Team_Id" });
            DropIndex("dbo.Messages", new[] { "Team_Id" });
            DropColumn("dbo.AspNetUsers", "Team_Id");
            DropColumn("dbo.Messages", "Team_Id");
            DropTable("dbo.Teams");
        }
    }
}
