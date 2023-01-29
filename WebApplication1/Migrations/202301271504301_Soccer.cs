namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Soccer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Url = c.String(),
                        Order = c.Int(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItems", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PasswordConfirm = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Position = c.String(nullable: false),
                        Check = c.String(nullable: false),
                        TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Coach = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.MenuItems", "ParentId", "dbo.MenuItems");
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.MenuItems", new[] { "ParentId" });
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.MenuItems");
        }
    }
}
