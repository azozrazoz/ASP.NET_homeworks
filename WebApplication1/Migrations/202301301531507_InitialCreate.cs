namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Gender = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Specialty = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Gender = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientDoctor",
                c => new
                    {
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PatientId, t.DoctorId })
                .ForeignKey("dbo.Doctors", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientDoctor", "DoctorId", "dbo.Patients");
            DropForeignKey("dbo.PatientDoctor", "PatientId", "dbo.Doctors");
            DropIndex("dbo.PatientDoctor", new[] { "DoctorId" });
            DropIndex("dbo.PatientDoctor", new[] { "PatientId" });
            DropTable("dbo.PatientDoctor");
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
        }
    }
}
