namespace LiveHackDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Technology_TechnologyId = c.Guid(),
                        Hackathon_HackathonId = c.Guid(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Technologies", t => t.Technology_TechnologyId)
                .ForeignKey("dbo.Hackathons", t => t.Hackathon_HackathonId)
                .ForeignKey("dbo.Institutions", t => t.GroupId)
                .Index(t => t.GroupId)
                .Index(t => t.Technology_TechnologyId)
                .Index(t => t.Hackathon_HackathonId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Institution_InstitutionId = c.Guid(),
                        Group_GroupId = c.Guid(),
                        Group_GroupId1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institutions", t => t.Institution_InstitutionId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Institution_InstitutionId)
                .Index(t => t.Group_GroupId)
                .Index(t => t.Group_GroupId1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Hackathons",
                c => new
                    {
                        HackathonId = c.Guid(nullable: false),
                        Name = c.String(),
                        ShortName = c.String(),
                        Description = c.String(),
                        StartDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        EndDateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        Institution_InstitutionId = c.Guid(),
                    })
                .PrimaryKey(t => t.HackathonId)
                .ForeignKey("dbo.Institutions", t => t.Institution_InstitutionId)
                .Index(t => t.Institution_InstitutionId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Guid(nullable: false),
                        Name = c.String(),
                        TeamGroup_GroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.TechnologyId)
                .ForeignKey("dbo.Groups", t => t.TeamGroup_GroupId)
                .Index(t => t.TeamGroup_GroupId);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Guid(nullable: false),
                        Name = c.String(),
                        ZipCode = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.InstitutionId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Guid(nullable: false),
                        Body = c.String(),
                        SendTime = c.DateTimeOffset(nullable: false, precision: 7),
                        Sender_Id = c.String(maxLength: 128),
                        Group_GroupId = c.Guid(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .Index(t => t.Sender_Id)
                .Index(t => t.Group_GroupId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.HackathonUsers",
                c => new
                    {
                        Hackathon_HackathonId = c.Guid(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Hackathon_HackathonId, t.User_Id })
                .ForeignKey("dbo.Hackathons", t => t.Hackathon_HackathonId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Hackathon_HackathonId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Group_GroupId1", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.HackathonUsers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.HackathonUsers", "Hackathon_HackathonId", "dbo.Hackathons");
            DropForeignKey("dbo.Hackathons", "Institution_InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.AspNetUsers", "Institution_InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Groups", "GroupId", "dbo.Institutions");
            DropForeignKey("dbo.Groups", "Hackathon_HackathonId", "dbo.Hackathons");
            DropForeignKey("dbo.Groups", "Technology_TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.Technologies", "TeamGroup_GroupId", "dbo.Groups");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.HackathonUsers", new[] { "User_Id" });
            DropIndex("dbo.HackathonUsers", new[] { "Hackathon_HackathonId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "Group_GroupId" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Technologies", new[] { "TeamGroup_GroupId" });
            DropIndex("dbo.Hackathons", new[] { "Institution_InstitutionId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_GroupId" });
            DropIndex("dbo.AspNetUsers", new[] { "Institution_InstitutionId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Groups", new[] { "Hackathon_HackathonId" });
            DropIndex("dbo.Groups", new[] { "Technology_TechnologyId" });
            DropIndex("dbo.Groups", new[] { "GroupId" });
            DropTable("dbo.HackathonUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Institutions");
            DropTable("dbo.Technologies");
            DropTable("dbo.Hackathons");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Groups");
        }
    }
}
