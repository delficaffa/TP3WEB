namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ALKSHNDKLAJSDK : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.CountryID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Surname = c.String(nullable: false, maxLength: 50, unicode: false),
                        Country = c.Int(nullable: false),
                        Date = c.DateTime(),
                        Turn = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Country", t => t.Country)
                .Index(t => t.Country);
            
            CreateTable(
                "dbo.Horarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        StartlHour = c.DateTime(nullable: false),
                        FinishHour = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Horarios", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Country", "dbo.Country");
            DropIndex("dbo.Horarios", new[] { "EmployeeId" });
            DropIndex("dbo.Employee", new[] { "Country" });
            DropTable("dbo.Horarios");
            DropTable("dbo.Employee");
            DropTable("dbo.Country");
        }
    }
}
