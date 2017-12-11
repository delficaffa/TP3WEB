namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
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
                        CheckIn = c.Time(precision: 7),
                        CheckOut = c.Time(precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Country", t => t.Country)
                .Index(t => t.Country);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "Country", "dbo.Country");
            DropIndex("dbo.Employee", new[] { "Country" });
            DropTable("dbo.Employee");
            DropTable("dbo.Country");
        }
    }
}
