namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Horarios : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Employee");
            CreateTable(
                "dbo.Horarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        StartlHour = c.DateTime(nullable: false),
                        FinishHour = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Employee", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Employee", "ID");
            CreateIndex("dbo.Employee", "ID");
            AddForeignKey("dbo.Employee", "ID", "dbo.Horarios", "ID");
            DropColumn("dbo.Employee", "CheckIn");
            DropColumn("dbo.Employee", "CheckOut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employee", "CheckOut", c => c.Time(precision: 7));
            AddColumn("dbo.Employee", "CheckIn", c => c.Time(precision: 7));
            DropForeignKey("dbo.Employee", "ID", "dbo.Horarios");
            DropIndex("dbo.Employee", new[] { "ID" });
            DropPrimaryKey("dbo.Employee");
            AlterColumn("dbo.Employee", "ID", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Horarios");
            AddPrimaryKey("dbo.Employee", "ID");
        }
    }
}
