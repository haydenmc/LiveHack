namespace LiveHackDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeInstitutionOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "GroupId", "dbo.Institutions");
            DropIndex("dbo.Groups", new[] { "GroupId" });
            AddColumn("dbo.Groups", "Institution_InstitutionId", c => c.Guid());
            CreateIndex("dbo.Groups", "Institution_InstitutionId");
            AddForeignKey("dbo.Groups", "Institution_InstitutionId", "dbo.Institutions", "InstitutionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groups", "Institution_InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Groups", new[] { "Institution_InstitutionId" });
            DropColumn("dbo.Groups", "Institution_InstitutionId");
            CreateIndex("dbo.Groups", "GroupId");
            AddForeignKey("dbo.Groups", "GroupId", "dbo.Institutions", "InstitutionId");
        }
    }
}
