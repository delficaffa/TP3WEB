namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullExitHour : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Horarios", "FinishHour", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Horarios", "FinishHour", c => c.DateTime(nullable: false));
        }
    }
}
