namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TrackingStates", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TrackingStates", "UserId", c => c.Int(nullable: false));
        }
    }
}
