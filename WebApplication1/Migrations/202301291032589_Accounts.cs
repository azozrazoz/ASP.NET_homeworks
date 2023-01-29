namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItems", "ParentId", "dbo.MenuItems");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropIndex("dbo.MenuItems", new[] { "ParentId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Gender = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Specialty = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        doctor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.doctor_Id)
                .Index(t => t.doctor_Id);
            
            DropTable("dbo.MenuItems");
            DropTable("dbo.Players");
            DropTable("dbo.Teams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Coach = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Accounts", "doctor_Id", "dbo.Accounts");
            DropIndex("dbo.Accounts", new[] { "doctor_Id" });
            DropTable("dbo.Accounts");
            CreateIndex("dbo.Players", "TeamId");
            CreateIndex("dbo.MenuItems", "ParentId");
            AddForeignKey("dbo.Players", "TeamId", "dbo.Teams", "Id");
            AddForeignKey("dbo.MenuItems", "ParentId", "dbo.MenuItems", "Id");
        }
    }
}
